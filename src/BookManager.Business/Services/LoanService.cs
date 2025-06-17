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

namespace BookManager.Business.Services;

public class LoanService(IBookService _bookService,
    IUserService _userService,
    ILoanRepository _loanRepository,
    IMapper _mapper,
    IValidator<Loan> _validator,
    IValidator<LoanFilterRequest> _validatorRequest,
    IPaymentProcessorStrategy _paymentProcessoStrategy,
    INotifier _notifier) : ILoanService
{

    public async Task<Result<bool>> CreateAsync(LoanRequest model)
    {
        _notifier.AddNotification(Issues.i1000, "Invoked CreateAsync method in LoanService.");
        var loan = _mapper.Map<Loan>(model);

        var resultValidation = _validator.Validate(loan);
        if (!resultValidation.IsValid)
        {
            _notifier.AddErrors(resultValidation);
            _notifier.AddError(Issues.e1000, "Validation of LoanRequest failed.");
            return resultValidation.ToFailureResult<bool>();
        }

        loan.Books = await ValidateAndReturnExistingBooks(loan);
        if (loan.Books.Count <= 0)
        {
            _notifier.AddError(Issues.e1001, "Validation of books failed.");
            return Result.Failure<bool>(new Error(Issues.e1001, "Validation of existing books failed."));
        }

        loan.User = await ValidateAndReturnExistingUser(loan);
        if (loan.User.Id == Guid.Empty)
        {
            _notifier.AddError(Issues.e1002, "Validation of existing user failed.");
            return Result.Failure<bool>(new Error(Issues.e1002, "Validation of existing user failed."));
        }

        await _loanRepository.CreateAsync(loan);

        return Result.Success(true);
    }

    private async Task<List<Book>> ValidateAndReturnExistingBooks(Loan model)
    {
        var bookIds = model.Books.Select(b => b.Id).ToList();

        var existingBooks = await _bookService
            .GetQueryAsync(b => bookIds.Contains(b.Id));

        return existingBooks?.Data?.ToList() ?? new List<Book>();
    }

    private async Task<Users> ValidateAndReturnExistingUser(Loan model)
    {
        var mapped = _mapper.Map<Users>((await _userService.GetByIdAsync(model.UserId)).Data ?? new());

        return mapped;
    }

    public async Task<PagedResult<LoanResponseList>> GetAllAsync(LoanFilterRequest loanFilterRequest, CancellationToken cancellationToken)
    {
        _notifier.AddNotification(Issues.i1001, "Invoked GetAllAsync method in LoanService.");

        var resultValidation = _validatorRequest.Validate(loanFilterRequest);
        if (!resultValidation.IsValid)
        {
            _notifier.AddErrors(resultValidation);
            _notifier.AddError(Issues.e1000, "Validation of loan filtering failed.");
            return resultValidation.ToFailurePagedResult<LoanResponseList>();
        }

        var result = await _loanRepository
         .QueryFilterPagedAsync(loanFilterRequest, cancellationToken);

        var loanResponse = _mapper.Map<PagedResult<LoanResponseList>>(result);

        return loanResponse;
    }

    public async Task<Result<RequestReturnBook>> RequestReturnBookAsync(Guid loanId)
    {
        _notifier.AddNotification(Issues.i1002, "Invoked RequestReturnBookAsync method in LoanService.");

        var loan = await _loanRepository.GetByIdAsync(loanId);
        if (loan == null)
            return Result.Failure<RequestReturnBook>(new Error(Issues.e1003, "Loan not found."));

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

    public async Task<Result<bool>> ReturnBookAsync(ReturnBookRequest returnBookRequest)
    {
        _notifier.AddNotification(Issues.i1003, "Invoked ReturnBookAsync method in LoanService.");

        var loan = await _loanRepository.GetByIdAsync(returnBookRequest.IdLoan);
        if (loan == null)
            return Result.Failure<bool>(new Error(Issues.e1004, "Loan not found."));

        await _paymentProcessoStrategy.ProcessPayment(new ApiPaymentRequest
        {
            Amount = returnBookRequest.Value,
        });

        loan.Status = LoanStatus.Completed;
        var updateResult = await _loanRepository.UpdateAsync(loan);
        if (!updateResult)
            return Result.Failure<bool>(new Error(Issues.e1005, "Updating loan failed."));

        return Result.Success(true);
    }
}
