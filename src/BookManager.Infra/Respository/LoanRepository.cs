using BookManager.Domain.Entity;
using BookManager.Domain.Interface.Repositories;
using BookManager.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace BookManager.Infra.Respository;

public class LoanRepository(BookManagerDbContext context) : ILoanRepository
{
    protected readonly DbSet<Loan> _dbSet = context.Set<Loan>();

    public async Task<bool> CreateAsync(Loan model)
    {
        try
        {
            _dbSet.Add(model);
            return await context.SaveChangesAsync() > 0;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<bool> DeleteAsync(Loan model)
    {
        try
        {
            EntityEntry<Loan> entry = context.Entry(model);
            _dbSet.Attach(model);

            entry.State = EntityState.Modified;

            return await context.SaveChangesAsync() > 0;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public IQueryable<Loan> Query(Expression<Func<Loan, bool>> where)
    {
        try
        {
            return _dbSet.Where(where);
        }
        catch (Exception)
        {
            throw;
        }
    }
}
