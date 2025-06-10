using BookManager.Domain.Entity;
using System.Linq.Expressions;

namespace BookManager.Domain.Interface.Repositories;
public interface ILoanRepository
{
    IQueryable<Loan> Query(Expression<Func<Loan,bool>> where);

    Task<bool> CreateAsync(Loan model);

    Task<bool> DeleteAsync(Loan model);

    Task<Loan> GetByIdAsync(Guid id);

    Task<bool> UpdateAsync (Loan model);
}
