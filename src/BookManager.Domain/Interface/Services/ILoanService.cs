using BookManager.Domain.Commom.Results;
using BookManager.Domain.Model.Loans;

namespace BookManager.Domain.Interface.Services;
public interface ILoanService
{
    Task<Result<bool>> CreateAsync(LoanRequest model);
    Task<Result<IEnumerable<LoanResponseList>>> GetAllAsync();
    Task<Result<RequestReturnBook>> RequestReturnBookAsync(Guid loanId);
    Task<Result<RequestReturnBook>> ReturnBookAsync(ReturnBookRequest returnBookRequest);
}
