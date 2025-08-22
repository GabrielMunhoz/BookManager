namespace BookManager.Domain.Model.Loans;
public class LoanRequest
{
    public Guid UserId { get; set; }

    public List<Guid> Books { get; set; } = [];
}
