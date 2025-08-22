namespace BookManager.Domain.Model.ApiPayment;
public class ApiPaymentResponse
{
    public int Id { get; set; }
    public string Message { get; set; }
    public bool Success { get; set; }
    public string TransactionReference { get; set; }
}
