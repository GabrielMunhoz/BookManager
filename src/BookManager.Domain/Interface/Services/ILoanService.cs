using BookManager.Domain.Commom.Results;
using BookManager.Domain.Model.Loans;

namespace BookManager.Domain.Interface.Services;
public interface ILoanService
{
    Task<Result<bool>> CreateAsync(LoanRequest model);
    Task<Result<List<LoanResponseList>>> GetAllAsync();
    Task<Result<RequestReturnBook>> RequestReturnBookAsync(Guid loanId);
    Task<Result<bool>> ReturnBookAsync(ReturnBookRequest returnBookRequest);
}
