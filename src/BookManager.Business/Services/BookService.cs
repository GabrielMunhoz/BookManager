using BookManager.Domain.Entity;
using BookManager.Domain.Interface.Repositories;
using BookManager.Domain.Interface.Services;

namespace BookManager.Business.Services;

public class BookService(IBookRepository bookRepository) : IBookService
{
    private readonly IBookRepository _bookRepository = bookRepository;

    public async Task<Book> CreateBook(Book model)
    {
        return await _bookRepository.CreateAsync(model);
    }

    public Task<IEnumerable<Book>> GetAllBooksAsync()
    {
        return Task.FromResult(_bookRepository
            .Query(b => b.Id != Guid.Empty)
            .AsEnumerable());
    }

    public async Task<Book> GetBookByIdAsync(Guid bookId)
    {
        return await _bookRepository.GetAsync(bookId) ?? new Book();
    }

    public async Task<Book> UpdateBook(Book model)
    {
        var updated =  await _bookRepository.UpdateAsync(model);

        return updated ? await _bookRepository.GetAsync(model.Id) ?? new Book() : new Book();
    }
}
