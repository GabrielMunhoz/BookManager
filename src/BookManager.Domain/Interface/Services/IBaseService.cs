using BookManager.Domain.Interface.Entities;

namespace BookManager.Domain.Interface.Services;
public interface IBaseService <TEntity> where TEntity : IBaseEntity
{
    Task<TEntity> GetByIdAsync(Guid bookId);
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity> CreateAsync(TEntity model);
    Task<TEntity> UpdateAsync(TEntity model);
    Task<bool> DeleteByIdAsync(Guid id);
}
