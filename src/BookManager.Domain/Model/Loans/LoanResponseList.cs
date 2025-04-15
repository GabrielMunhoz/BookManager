using BookManager.Domain.Entity;
using BookManager.Domain.Model.Books;
using BookManager.Domain.Model.User;

namespace BookManager.Domain.Model.Loans;
public class LoanResponseList
{
    public Guid Id { get; set; }
    public UserResponseList User { get; set; }
    public DateTime LoanDate { get; set; }
    public List<BookResponseList> Books { get; set; } = [];
}
