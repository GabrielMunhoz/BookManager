using BookManager.Domain.Entity;
using BookManager.Domain.Interface.Repositories.Base;
using BookManager.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using System.Data.Common;
using System.Linq.Expressions;

namespace BookManager.Infra.Respository.Base;

public class BaseRepository<TEntity>(BookManagerDbContext context, ILogger<BaseRepository<TEntity>> logger) : IBaseRespository<TEntity> where TEntity : BaseEntity
{
    protected DbSet<TEntity> DbSet => _context.Set<TEntity>();

    private readonly BookManagerDbContext _context = context;
    private readonly ILogger<BaseRepository<TEntity>> _logger = logger;

    public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> where)
    {
        try
        {
            return DbSet.Where(where);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, object> includes)
    {
        try
        {
            IQueryable<TEntity>? _query = DbSet;

            if (includes != null)
                _query = includes(_query) as IQueryable<TEntity>;

            return _query.Where(predicate).AsQueryable();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> where)
    {
        try
        {
            return await DbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(where);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public async Task<TEntity?> GetByIdAsync(Guid id)
    {
        try
        {
            return await DbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public async Task<TEntity> CreateAsync(TEntity model)
    {
        try
        {
            DbSet.Add(model);
            await SaveAsync();
            return model;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public async Task<bool> UpdateAsync(TEntity model)
    {
        try
        {
            var existingEntity =
                await _context.Set<TEntity>().AsNoTracking().FirstAsync(x => x.Id == model.Id) ??
                    throw new InvalidOperationException("Entity not found.");

            _context.Entry(model).State = EntityState.Modified;

            return await SaveAsync() > 0;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public async Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> where)
    {
        try
        {
            TEntity? model = await GetAsync(where);

            return (model != null) && await DeleteAsync(model);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public async Task<bool> DeleteAsync(TEntity model)
    {
        try
        {
            EntityEntry<TEntity> entry = _context.Entry(model);

            DbSet.Attach(model);

            entry.State = EntityState.Deleted;

            return await SaveAsync() > 0;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public async Task<int> SaveAsync()
    {
        try
        {
            return await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public void Dispose()
    {
        try
        {
            _context?.Dispose();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }
}
