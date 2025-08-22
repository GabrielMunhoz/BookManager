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

public class LoanService(
    IBookRepository _bookRepository,
    IUserRepository _userRepository,
    ILoanRepository _loanRepository,
    IMapper _mapper,
    IValidator<Loan> _validator,
    IValidator<LoanFilterRequest> _validatorRequest,
    IPaymentProcessorStrategy _paymentProcessoStrategy,
    INotifier _notifier) : ILoanService
{

    public async Task<Result<bool>> CreateAsync(LoanRequest model, CancellationToken cancellationToken)
    {
        _notifier.AddNotification(Issues.i1000, "Invoked CreateAsync method in LoanService.");
        var loan = _mapper.Map<Loan>(model);

        var resultValidation = await _validator.ValidateAsync(loan);
        if (!resultValidation.IsValid)
        {
            _notifier.AddErrors(resultValidation);
            _notifier.AddError(Issues.e1000, "Validation of LoanRequest failed.");
            return resultValidation.ToFailureResult<bool>();
        }

        try
        {
            using var transaction = _loanRepository.CreateTransactionAsync(cancellationToken);

            loan.Books = _bookRepository.Query(b => model.Books.Contains(b.Id)).ToList();

            loan.Books
                .ForEach(x => x.Stock = x.Stock--);

            // TODO - Avaliar porque salva somente um usuario no loan
            loan.User = await _userRepository.GetByIdAsync(model.UserId);

            loan.TotalValue = loan.Books.Select(x => x.Value).Sum();

            var result = await _loanRepository.CreateAsync(loan);

            if (!result)
            {
                await _loanRepository.RollbackAsync(cancellationToken);
                _notifier.AddError(Issues.e1015, "Error on creating loan");
                return resultValidation.ToFailureResult<bool>();
            }

            await _loanRepository.CommitAsync(cancellationToken);

            return Result.Success(true);
        }
        catch (Exception ex)
        {
            await _loanRepository.RollbackAsync(cancellationToken);
            _notifier.AddError(Issues.e1015, ex.Message);
            return resultValidation.ToFailureResult<bool>();
        }
    }

    public async Task<PagedResult<LoanResponseList>> GetAllAsync(LoanFilterRequest loanFilterRequest, CancellationToken cancellationToken)
    {
        _notifier.AddNotification(Issues.i1001, "Invoked GetAllAsync method in LoanService.");

        var resultValidation = await _validatorRequest.ValidateAsync(loanFilterRequest);
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

        loan.PayedValue = returnBookRequest.Value;
        loan.Status = LoanStatus.Completed;

        var updateResult = await _loanRepository.UpdateAsync(loan);
        if (!updateResult)
            return Result.Failure<bool>(new Error(Issues.e1005, "Updating loan failed."));

        return Result.Success(true);
    }
}
