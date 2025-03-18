using BookManager.Domain.Interface.Entities;
using System.Linq.Expressions;

namespace BookManager.Domain.Interface.Repositories.Base;

public interface IBaseRespository<TEntity> : IDisposable where TEntity : IBaseEntity
{
    IQueryable<TEntity> QueryAsync(Expression<Func<TEntity, bool>> where);
    
    IQueryable<TEntity> QueryAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, object> includes);

    Task<TEntity?> GetAsync(params object[] Keys);

    Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> where);

    Task<TEntity> CreateAsync(TEntity model);

    Task<bool> UpdateAsync(TEntity model);

    Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> where);

    Task<bool> DeleteAsync(TEntity model);

    Task<bool> DeleteAsync(params object[] Keys);
    
    Task<int> SaveAsync();
}
