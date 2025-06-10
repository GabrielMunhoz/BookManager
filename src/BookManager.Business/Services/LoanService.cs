using AutoMapper;
using BookManager.Business.Strategy.Loans.Interface;
using BookManager.Business.Strategy.Loans.ReturnBookCalculation;
using BookManager.Domain.Commom.Enums;
using BookManager.Domain.Commom.Results;
using BookManager.Domain.Entity;
using BookManager.Domain.Extensions;
using BookManager.Domain.Interface.Common;
using BookManager.Domain.Interface.Repositories;
using BookManager.Domain.Interface.Services;
using BookManager.Domain.Model.ApiPayment;
using BookManager.Domain.Model.Loans;
using BookManager.Infra.ApiServices.Interfaces;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace BookManager.Business.Services;

public class LoanService(IBookService bookService,
    IUserService userService,
    ILoanRepository loanRepository,
    IMapper mapper,
    IValidator<Loan> validator,
    ILogger<LoanService> logger, 
    IPaymentProcessorStrategy paymentProcessoStrategy, 
    INotifier notifier) : ILoanService
{
    private readonly IUserService _userService = userService;
    private readonly ILoanRepository _loanRepository = loanRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IValidator<Loan> _validator = validator;
    private readonly ILogger<LoanService> _logger = logger;
    private readonly IPaymentProcessorStrategy _paymentProcessoStrategy = paymentProcessoStrategy;
    private readonly INotifier _notifier = notifier;
    private readonly IBookService _bookService = bookService;

    public async Task<Result<bool>> CreateAsync(LoanRequest model)
    {
        _logger.LogInformation("Invoked CreateAsync method from LoanService");
        var loan = _mapper.Map<Loan>(model);

        var resultValidation = _validator.Validate(loan);
        if (!resultValidation.IsValid)
        {
            _notifier.AddErrors(resultValidation);
            var result = resultValidation.ToFailureResult<bool>();
            _logger.LogInformation($"Validate of LoanRequest fail, data: {result.Errors}");
            return result;
        }

        ValidateBooksExisting(loan);

        await ValidateUserExists(loan);

        await _loanRepository.CreateAsync(loan);

        return Result.Success(true);
    }

    private void ValidateBooksExisting(Loan model)
    {
        var bookIds = model.Books.Select(b => b.Id).ToList();

        var existingBooks = _bookService
            .GetQueryAsync(b => bookIds.Contains(b.Id))
            .Result.Data
            .ToList();

        model.Books = existingBooks;
    }

    private async Task ValidateUserExists(Loan model)
    {
        model.User = await _userService.GetByIdAsync(model.UserId) ?? throw new InvalidOperationException();
    }

    public async Task<Result<IEnumerable<LoanResponseList>>> GetAllAsync()
    {
        var result = _loanRepository
         .Query(l => l.Id != Guid.Empty)
         .AsEnumerable();
        var loanResponse = _mapper.Map<IEnumerable<LoanResponseList>>(result);
        return Result.Success(loanResponse);
    }

    public async Task<Result<RequestReturnBook>> RequestReturnBookAsync(Guid loanId)
    {
        var loan = await _loanRepository.GetByIdAsync(loanId);
        if (loan == null)
            return Result.Failure<RequestReturnBook>(new Error("Loan not found"));

        return Result.Success(ProcessReturnBookRequest(loan));
    }

    private RequestReturnBook ProcessReturnBookRequest(Loan loan)
    {
        IReturnBookCalculationStrategy strategy = GetStrategyByReturnStatus(loan);

        return strategy.Calculate(loan);
    }

    private static IReturnBookCalculationStrategy GetStrategyByReturnStatus(Loan loan)
    {
        var status = loan.ReturnDate.Date.CompareTo(DateTime.Now.Date);

        return status switch
        {
            < 0 => new InterestStrategy(),
            > 0 => new DiscountStrategy(),
            _ => new NoChangeStrategy()
        };
    }

    public async Task<Result<RequestReturnBook>> ReturnBookAsync(ReturnBookRequest returnBookRequest)
    {
        var loan = await _loanRepository.GetByIdAsync(returnBookRequest.IdLoan);
        if (loan == null)
            return Result.Failure<RequestReturnBook>(new Error("Loan not found"));

        await _paymentProcessoStrategy.ProcessPayment(new ApiPaymentRequest
        {
            Amount = returnBookRequest.Value,
        });

        loan.Status = LoanStatus.Completed;
        var updateResult = await _loanRepository.UpdateAsync(loan);
        if(!updateResult)
            return Result.Failure<RequestReturnBook>(new Error("Update loan failde"));

        return Result.Success(new RequestReturnBook
        {
            Message = "Request for return book completed.",
        });
    }
}
