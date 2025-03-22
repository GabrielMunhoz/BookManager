namespace BookManager.Domain.Entity;

public class Loan: BaseEntity
{
    public Guid UserId { get; set; }
    public DateTime LoanDate { get; set; }
    public UserBook UserBook { get; set; }

    public List<Book> Books { get; set; } = [];
}
