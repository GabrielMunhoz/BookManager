namespace BookManager.Domain.Model.Loans;
public class LoanRequest
{
    public Guid UserId { get; set; }
    public DateTime LoanDate { get; set; } = DateTime.Now;

    public List<Guid> Books { get; set; } = [];
}
