namespace BookManager.Domain.Model.Loans;

public class ReturnBookRequest()
{
    public Guid IdLoan { get; set; }
    public decimal Value { get; set; }
}