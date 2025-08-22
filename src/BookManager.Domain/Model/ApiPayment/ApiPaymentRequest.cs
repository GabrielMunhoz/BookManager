namespace BookManager.Domain.Model.ApiPayment;
public class ApiPaymentRequest
{
    public decimal Amount { get; set; }
    public string Currency { get; set; } = "USD";
    public string PaymentMethod { get; set; } = "CreditCard";
    public string CardNumber { get; set; }
    public string CardHolder { get; set; }
    public string Expiry { get; set; }
    public string Cvv { get; set; }
}