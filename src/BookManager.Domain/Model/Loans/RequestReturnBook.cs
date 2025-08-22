namespace BookManager.Domain.Model.Loans;

public class RequestReturnBook()
{
    public string Message { get; set; }
    public decimal PercentInterestOrdiscount { get; set; }
    public decimal Value { get; set; }
    public decimal ValueCalculated { get; set; }
}