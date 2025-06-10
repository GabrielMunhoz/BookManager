using BookManager.Domain.Commom.Results;
using BookManager.Domain.Entity;
using System.Linq.Expressions;

namespace BookManager.Domain.Interface.Services;
public interface IBookService : IBaseService<Book>
{
    Task<Result<IEnumerable<Book>>> GetQueryAsync(Expression<Func<Book, bool>> where);
}
