namespace BookManager.Domain.Entity;

public class Loan: BaseEntity
{
    public Guid IdUser { get; set; }
    public DateTime LoanDate { get; set; }
    public UserBook UserBook { get; set; }
    public IList<Book> Books { get; set; }
}
