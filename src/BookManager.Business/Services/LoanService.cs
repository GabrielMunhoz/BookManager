using BookManager.Domain.Entity;
using BookManager.Domain.Interface.Repositories;
using BookManager.Domain.Interface.Services;

namespace BookManager.Business.Services;

public class LoanService(IBookService bookService, 
    IUserBookService userBookService, 
    ILoanRepository loanRepository) : ILoanService
{
    private readonly IUserBookService _userBookService = userBookService;
    private readonly ILoanRepository _loanRepository = loanRepository;
    private readonly IBookService _bookService = bookService;

    public async Task<bool> CreateAsync(Loan model)
    {
        ValidateBooksExisting(model);

        if (!await UserBooksExists(model.UserId))
            return false;

        await _loanRepository.CreateAsync(model);

        return true;
    }

    private void ValidateBooksExisting(Loan model)
    {
        var bookIds = model.Books.Select(b => b.Id).ToList();

        var existingBooks = _bookService
            .GetQuery(b => bookIds.Contains(b.Id))
            .ToList();

        model.Books = existingBooks;
    }

    private async Task<bool> UserBooksExists(Guid UserId)
    {
        return await _userBookService.GetByIdAsync(UserId) != null;
    }

    public Task<bool> DeleteAsync(Loan model)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Loan> GetAll()
    {
        return _loanRepository
            .Query(l => l.Id != Guid.Empty)
            .AsEnumerable();
    }
}
