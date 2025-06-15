using BookManager.Domain.Commom.Results;
using BookManager.Domain.Entity;
using BookManager.Domain.Model.Books;
using System.Linq.Expressions;

namespace BookManager.Domain.Interface.Services;
public interface IBookService
{
    Task<Result<IEnumerable<Book>>> GetQueryAsync(Expression<Func<Book, bool>> where);
    Task<Result<BookDetail>> GetByIdAsync(Guid bookId);
    Task<Result<IEnumerable<BookList>>> GetAllAsync();
    Task<Result<bool>> CreateAsync(BookCreate bookCreate);
    Task<Result<bool>> UpdateAsync(BookUpdate bookUpdate);
    Task<Result<bool>> DeleteByIdAsync(Guid id);
}
