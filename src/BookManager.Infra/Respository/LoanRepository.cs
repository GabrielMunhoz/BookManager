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
            foreach (var item in model.Books)
                context.Entry(item).State = EntityState.Unchanged;

            context.Entry(model.UserBook).State = EntityState.Unchanged;

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
            return _dbSet.Where(where).Include(x => x.UserBook).Include(x => x.UserBook);
        }
        catch (Exception)
        {
            throw;
        }
    }
}
