using BookManager.Domain.Commom.Enums;
using BookManager.Domain.Commom.Results;
using BookManager.Domain.Entity;
using BookManager.Domain.Interface.Repositories;
using BookManager.Domain.Model.Loans;
using BookManager.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

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

    public async Task<PagedResult<Loan>> QueryFilterPagedAsync(LoanFilterRequest loanFilterRequest, CancellationToken cancellationToken)
    {
        try
        {
            IQueryable<Loan> query = GetQueryFiltered(loanFilterRequest);

            int totalCount = await query.CountAsync(cancellationToken);
            int totalPages = (int)Math.Ceiling((double)totalCount / loanFilterRequest.PageSize);

            var resultPaged = await query.Skip((loanFilterRequest.Page - 1) * loanFilterRequest.PageSize)
                .Take(loanFilterRequest.PageSize)
                .ToListAsync(cancellationToken);

            var result = PagedResult<Loan>.Success(resultPaged);

            result.TotalPage = totalPages;
            result.TotalCount = totalCount;
            result.Page = loanFilterRequest.Page;
            result.PageSize = loanFilterRequest.PageSize;

            return result;

        }
        catch (Exception)
        {
            throw;
        }
    }

    private IQueryable<Loan> GetQueryFiltered(LoanFilterRequest loanFilterRequest)
    {
        return _dbSet
            .Include(x => x.User)
            .Include(x => x.Books)
            .Where(
                loan =>
                (string.IsNullOrEmpty(loanFilterRequest.UserName) || loan.User.Name.ToLower().Contains(loanFilterRequest.UserName.ToLower())) &&
                (string.IsNullOrEmpty(loanFilterRequest.UserEmail) || loan.User.Email.ToLower().Contains(loanFilterRequest.UserEmail.ToLower())) &&
                (string.IsNullOrEmpty(loanFilterRequest.BookTitle) || loan.Books.Any(x => x.Title.ToLower().Contains(loanFilterRequest.BookTitle.ToLower()))) &&
                ((!loanFilterRequest.StatusLoan.Any() && loan.Status != LoanStatus.Completed) || loanFilterRequest.StatusLoan.Contains(loan.Status)) &&
                (((!loanFilterRequest.InitialReturnDate.HasValue && !loanFilterRequest.FinalReturnDate.HasValue) ||
                    loan.ReturnDate >= loanFilterRequest.InitialReturnDate && loan.ReturnDate <= loanFilterRequest.FinalReturnDate)) &&
                (((!loanFilterRequest.InitialCreateDate.HasValue && !loanFilterRequest.FinalCreateDate.HasValue) ||
                    loan.CreateDate >= loanFilterRequest.InitialCreateDate && loan.CreateDate <= loanFilterRequest.FinalCreateDate))
            )
            .AsNoTracking()
            .OrderByDescending(x => x.CreateDate)
            .AsQueryable();
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

    public async Task<IDbContextTransaction> CreateTransactionAsync(CancellationToken cancellationToken)
    => await context.Database.BeginTransactionAsync(cancellationToken);

    public async Task CommitAsync(CancellationToken cancellationToken)
        => await context.Database.CommitTransactionAsync(cancellationToken);

    public async Task RollbackAsync(CancellationToken cancellationToken)
       => await context.Database.RollbackTransactionAsync(cancellationToken);
}
