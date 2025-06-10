using BookManager.Domain.Commom.Enums;

namespace BookManager.Domain.Entity;

public class Loan : BaseEntity
{
    public Guid UserId { get; set; }
    public DateTime ReturnDate { get; set; } = DateTime.Now.AddDays(7);
    public Users User { get; set; }
    public LoanStatus Status { get; set; }

    public List<Book> Books { get; set; } = [];
}
