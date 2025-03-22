using BookManager.Domain.Entity;

namespace BookManager.Domain.Interface.Services;
public interface ILoanService
{
    Task<bool> CreateAsync(Loan model);
    Task<bool> DeleteAsync(Loan model);
    IEnumerable<Loan> GetAll();
}
