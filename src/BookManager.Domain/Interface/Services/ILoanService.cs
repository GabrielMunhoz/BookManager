using BookManager.Domain.Entity;
using BookManager.Domain.Model.Loans;

namespace BookManager.Domain.Interface.Services;
public interface ILoanService
{
    Task<bool> CreateAsync(LoanRequest model);
    Task<bool> DeleteAsync(Loan model);
    IEnumerable<LoanResponseList> GetAll();
}
