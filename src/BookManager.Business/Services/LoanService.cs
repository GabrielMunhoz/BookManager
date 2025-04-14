using BookManager.Domain.Entity;
using BookManager.Domain.Interface.Repositories;
using BookManager.Domain.Interface.Services;
using BookManager.Domain.Model;

namespace BookManager.Business.Services;

public class LoanService(IBookService bookService,
    IUserBookService userBookService,
    ILoanRepository loanRepository) : ILoanService
{
    private readonly IUserBookService _userBookService = userBookService;
    private readonly ILoanRepository _loanRepository = loanRepository;
    private readonly IBookService _bookService = bookService;

    public async Task<bool> CreateAsync(LoanRequest model)
    {
        ArgumentNullException.ThrowIfNull(model);

        Loan loan = new()
        {
            LoanDate = model.LoanDate,
            UserId = model.UserId,
            Books = new()
        };

        loan.Books.AddRange(model.Books.Select(x => new Book { Id = Guid.Parse(x) }));

        ValidateBooksExisting(loan);

        await ValidateUserBooksExists(loan);

        await _loanRepository.CreateAsync(loan);

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

    private async Task ValidateUserBooksExists(Loan model)
    {
        model.UserBook = await _userBookService.GetByIdAsync(model.UserId) ?? throw new InvalidOperationException();
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
