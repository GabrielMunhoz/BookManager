using BookManager.Domain.Commom.Results;
using BookManager.Domain.Model.Loans;

namespace BookManager.Domain.Interface.Services;
public interface ILoanService
{
    Task<Result<bool>> CreateAsync(LoanRequest model, CancellationToken cancellationToken);
    Task<PagedResult<LoanResponseList>> GetAllAsync(LoanFilterRequest loanFilterRequest, CancellationToken cancellationToken);
    Task<Result<RequestReturnBook>> RequestReturnBookAsync(Guid loanId);
    Task<Result<bool>> ReturnBookAsync(ReturnBookRequest returnBookRequest);
}
