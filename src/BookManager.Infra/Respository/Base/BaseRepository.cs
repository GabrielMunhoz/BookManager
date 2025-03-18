using BookManager.Domain.Entity;
using BookManager.Domain.Interface.Repositories.Base;
using BookManager.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace BookManager.Infra.Respository.Base;

public class BaseRepository<TEntity>(BookManagerDbContext context) : IBaseRespository<TEntity> where TEntity : BaseEntity
{
    protected DbSet<TEntity> DbSet => _context.Set<TEntity>();

    private readonly BookManagerDbContext _context = context;

    public IQueryable<TEntity> QueryAsync(Expression<Func<TEntity, bool>> where)
    {
        try
        {
            return DbSet.Where(where);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public IQueryable<TEntity> QueryAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, object> includes)
    {
        try
        {
            IQueryable<TEntity>? _query = DbSet;

            if(includes != null)
                _query = includes(_query) as IQueryable<TEntity>;

            return _query.Where(predicate).AsQueryable();
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<TEntity?> GetAsync(params object[] Keys)
    {
        try
        {
            return await DbSet.FindAsync(Keys);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> where)
    {
        try
        {
            return await DbSet.AsNoTracking().FirstOrDefaultAsync(where);
        }
        catch (Exception)
        {
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
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<bool> UpdateAsync(TEntity model)
    {
        try
        {
            EntityEntry<TEntity> entry = _context.Entry(model);
            DbSet.Attach(model);

            entry.State = EntityState.Modified;

            return await SaveAsync() > 0;
        }
        catch (Exception)
        {
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
        catch (Exception)
        {
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
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<bool> DeleteAsync(params object[] Keys)
    {
        try
        {
            TEntity? model = await GetAsync(Keys); 

            return (model != null) && await DeleteAsync(model);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<int> SaveAsync()
    {
        try
        {
            return await _context.SaveChangesAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public void Dispose()
    {
        try
        {
            _context?.Dispose();
        }
        catch (Exception)
        {
            throw;
        }
    }
}
