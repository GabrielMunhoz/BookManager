namespace BookManager.Domain.Model;
public class LoanRequest
{
    public Guid UserId { get; set; }
    public DateTime LoanDate { get; set; } = DateTime.Now;

    public List<string> Books { get; set; } = [];
}
