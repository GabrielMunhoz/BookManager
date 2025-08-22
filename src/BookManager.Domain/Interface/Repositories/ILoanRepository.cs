using BookManager.Domain.Commom.Results;
using BookManager.Domain.Entity;
using BookManager.Domain.Model.Loans;
using Microsoft.EntityFrameworkCore.Storage;

namespace BookManager.Domain.Interface.Repositories;
public interface ILoanRepository
{
    Task<PagedResult<Loan>> QueryFilterPagedAsync(LoanFilterRequest loanFilterRequest, CancellationToken cancellationToken);

    Task<bool> CreateAsync(Loan model);

    Task<bool> DeleteAsync(Loan model);

    Task<Loan> GetByIdAsync(Guid id);

    Task<bool> UpdateAsync (Loan model);

    Task<IDbContextTransaction> CreateTransactionAsync(CancellationToken cancellationToken);

    Task CommitAsync(CancellationToken cancellationToken);

    Task RollbackAsync(CancellationToken cancellationToken);

}
