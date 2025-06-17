using BookManager.Domain.Commom.Results;
using BookManager.Domain.Entity;
using BookManager.Domain.Model.Loans;

namespace BookManager.Domain.Interface.Repositories;
public interface ILoanRepository
{
    Task<PagedResult<Loan>> QueryFilterPagedAsync(LoanFilterRequest loanFilterRequest, CancellationToken cancellationToken);

    Task<bool> CreateAsync(Loan model);

    Task<bool> DeleteAsync(Loan model);

    Task<Loan> GetByIdAsync(Guid id);

    Task<bool> UpdateAsync (Loan model);
}
