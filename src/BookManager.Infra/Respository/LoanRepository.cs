using BookManager.Domain.Entity;
using BookManager.Domain.Interface.Repositories;
using BookManager.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace BookManager.Infra.Respository;

public class LoanRepository(BookManagerDbContext context, ILogger<LoanRepository> logger) : ILoanRepository
{
    protected readonly DbSet<Loan> _dbSet = context.Set<Loan>();
    private readonly ILogger<LoanRepository> _logger = logger;

    public async Task<bool> CreateAsync(Loan model)
    {
        try
        {
            context.Entry(model.User).State = EntityState.Unchanged;

            _dbSet.Add(model);
            return await context.SaveChangesAsync() > 0;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error on CreateAsync");
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
            return _dbSet.Where(where)
                .AsNoTracking()
                .Include(x => x.User)
                .Include(x => x.Books);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<Loan> GetByIdAsync(Guid id)
    {
        return await _dbSet
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<bool> UpdateAsync(Loan model)
    {
        try
        {
            _dbSet.Attach(model);
            var entry = context.Entry(model);
            entry.State = EntityState.Modified;

            if (model.User != null)
                context.Entry(model.User).State = EntityState.Unchanged;

            if (model.Books != null)
            {
                await entry.Collection(l => l.Books).LoadAsync();
                entry.Collection(l => l.Books).CurrentValue = model.Books;
            }

            return await context.SaveChangesAsync() > 0;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error on UpdateAsync");
            throw;
        }
    }
}
