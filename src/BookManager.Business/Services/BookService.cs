using BookManager.Domain.Commom.Results;
using BookManager.Domain.Entity;
using BookManager.Domain.Interface.Repositories;
using BookManager.Domain.Interface.Services;
using System.Linq.Expressions;

namespace BookManager.Business.Services;

public class BookService(IBookRepository bookRepository) : IBookService
{
    private readonly IBookRepository _bookRepository = bookRepository;

    public async Task<Book> CreateAsync(Book model)
    {
        return await _bookRepository.CreateAsync(model);
    }

    public Task<IEnumerable<Book>> GetAllAsync()
    {
        return Task.FromResult(_bookRepository
            .Query(b => b.Id != Guid.Empty)
            .AsEnumerable());
    }

    public async Task<Book> GetByIdAsync(Guid bookId)
    {
        return await _bookRepository.GetAsync(b => b.Id == bookId) ?? new Book();
    }

    public async Task<Book> UpdateAsync(Book model)
    {
        var updated =  await _bookRepository.UpdateAsync(model);

        return updated ? await _bookRepository.GetAsync(b => b.Id == model.Id) ?? new Book() : new Book();
    }

    public async Task<bool> DeleteByIdAsync(Guid id)
    {
        return await _bookRepository.DeleteAsync(b => b.Id == id);
    }

    public Task<Result<IEnumerable<Book>>> GetQueryAsync(Expression<Func<Book, bool>> where)
    {
        return Task.FromResult(Result.Success(_bookRepository.Query(where).AsEnumerable()));
    }
}
