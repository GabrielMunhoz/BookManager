using AutoMapper;
using BookManager.Domain.Entity;
using BookManager.Domain.Interface.Repositories;
using BookManager.Domain.Interface.Services;
using BookManager.Domain.Model.Loans;

namespace BookManager.Business.Services;

public class LoanService(IBookService bookService,
    IUserService userService,
    ILoanRepository loanRepository,
    IMapper mapper) : ILoanService
{
    private readonly IUserService _userService = userService;
    private readonly ILoanRepository _loanRepository = loanRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IBookService _bookService = bookService;

    public async Task<bool> CreateAsync(LoanRequest model)
    {
        ArgumentNullException.ThrowIfNull(model);

        var loan = _mapper.Map<Loan>(model);

        ValidateBooksExisting(loan);

        await ValidateUserExists(loan);

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

    private async Task ValidateUserExists(Loan model)
    {
        model.User = await _userService.GetByIdAsync(model.UserId) ?? throw new InvalidOperationException();
    }

    public Task<bool> DeleteAsync(Loan model)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<LoanResponseList> GetAll()
    {
        var result = _loanRepository
         .Query(l => l.Id != Guid.Empty)
         .AsEnumerable(); 
        return _mapper.Map<IEnumerable<LoanResponseList>>(result);
    }
}
