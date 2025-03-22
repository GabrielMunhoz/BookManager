using BookManager.Domain.Entity;
using System.Linq.Expressions;

namespace BookManager.Domain.Interface.Services;
public interface IBookService : IBaseService<Book>
{
    IEnumerable<Book> GetQuery(Expression<Func<Book, bool>> where);
}
