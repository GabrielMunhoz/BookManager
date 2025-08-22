using BookManager.Domain.Interface.Entities;
using System.Linq.Expressions;

namespace BookManager.Domain.Interface.Repositories.Base;

public interface IBaseRespository<TEntity> : IDisposable where TEntity : IBaseEntity
{
    IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> where);
    
    IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, object> includes);

    Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> where);

    Task<TEntity?> GetByIdAsync(Guid id);

    Task<TEntity> CreateAsync(TEntity model);

    Task<bool> UpdateAsync(TEntity model);

    Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> where);

    Task<bool> DeleteAsync(TEntity model);

    Task<int> SaveAsync();
}
