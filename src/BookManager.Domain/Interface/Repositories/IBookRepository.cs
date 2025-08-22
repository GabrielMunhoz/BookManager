using BookManager.Domain.Entity;
using BookManager.Domain.Interface.Repositories.Base;

namespace BookManager.Domain.Interface.Repositories;

public interface IBookRepository : IBaseRespository<Book>
{
    Task<bool> IsInStockAsync(Guid bookId);
}
