using AutoMapper;
using BookManager.Domain.Commom.Enums;
using BookManager.Domain.Commom.Results;
using BookManager.Domain.Entity;
using BookManager.Domain.Extensions;
using BookManager.Domain.Interface.Common;
using BookManager.Domain.Interface.Repositories;
using BookManager.Domain.Interface.Services;
using BookManager.Domain.Model.Books;
using FluentValidation;
using System.Linq.Expressions;

namespace BookManager.Business.Services;

public class BookService(IBookRepository _bookRepository,
    IMapper _mapper,
    IValidator<Book> _validator,
    INotifier _notifier) : IBookService
{
    public async Task<Result<bool>> CreateAsync(BookCreate bookCreate)
    {
        _notifier.AddNotification(Issues.i1004, "Invoked CreateAsync method from BookService");

        var model = _mapper.Map<Book>(bookCreate);

        var validatorResult = _validator.Validate(model);

        if (!validatorResult.IsValid)
            return validatorResult.ToFailureResult<bool>();

        var result = await _bookRepository.CreateAsync(model);

        if (result?.Id == Guid.Empty)
            return Result.Failure<bool>(new Error(Issues.e1006, "Create book failed."));

        return Result.Success(true);
    }

    public Task<Result<IEnumerable<BookList>>> GetAllAsync()
    {
        _notifier.AddNotification(Issues.i1005, "Invoked GetAllAsync method from BookService");

        var result = _bookRepository
            .Query(b => b.Id != Guid.Empty)
            .AsEnumerable();

        var mapped = _mapper.Map<IEnumerable<BookList>>(result);

        return Task.FromResult(Result.Success(mapped));
    }

    public async Task<Result<BookDetail>> GetByIdAsync(Guid bookId)
    {
        _notifier.AddNotification(Issues.i1006, "Invoked GetByIdAsync method from BookService");

        var result = await _bookRepository.GetAsync(b => b.Id == bookId) ?? new Book();

        if (result.Id == Guid.Empty)
            return Result.Failure<BookDetail>(new Error(Issues.e1007, "Book not found"));

        var mapped = _mapper.Map<BookDetail>(result);

        return Result.Success(mapped);
    }

    public async Task<Result<bool>> UpdateAsync(BookUpdate bookCreate)
    {
        _notifier.AddNotification(Issues.i1007, "Invoked UpdateAsync method from BookService");

        var model = _mapper.Map<Book>(bookCreate);

        var validatorResult = _validator.Validate(model);

        if (!validatorResult.IsValid)
            return validatorResult.ToFailureResult<bool>();

        var updated = await _bookRepository.UpdateAsync(model);
        if (!updated)
            return Result.Failure<bool>(new Error(Issues.e1008, "Update book failed"));

        return Result.Success(true);
    }

    public async Task<Result<bool>> DeleteByIdAsync(Guid id)
    {
        _notifier.AddNotification(Issues.i1008, "Invoked DeleteByIdAsync method from BookService");

        var deleted = await _bookRepository.DeleteAsync(b => b.Id == id);

        if (!deleted)
            return Result.Failure<bool>(new Error(Issues.e1009, "Delete book failed"));

        return Result.Success(true);
    }

    public Task<Result<IEnumerable<Book>>> GetQueryAsync(Expression<Func<Book, bool>> where)
    {
        return Task.FromResult(Result.Success(_bookRepository.Query(where).AsEnumerable()));
    }
}
