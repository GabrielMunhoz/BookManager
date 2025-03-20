using BookManager.Domain.Entity;

namespace BookManager.Domain.Interface.Services;
public interface IBookService
{
    Task<Book> GetBookByIdAsync(Guid bookId);
    Task<IEnumerable<Book>> GetAllBooksAsync();
    Task<Book> CreateBook(Book model);
    Task<Book> UpdateBook(Book model);
}
