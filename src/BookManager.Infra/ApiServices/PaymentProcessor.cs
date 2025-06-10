using BookManager.Domain.Commom.Results;
using BookManager.Domain.Model.ApiPayment;
using BookManager.Infra.ApiServices.Interfaces;

namespace BookManager.Infra.ApiServices;
public class PaymentProcessor : IPaymentProcessorStrategy
{
    public async Task<Result<ApiPaymentResponse>> ProcessPayment(ApiPaymentRequest requestPayment)
    {
        return Result.Success(new ApiPaymentResponse
        { 
            Id = 1,
            Message = "Payment proccessed", 
            Success = true,
            TransactionReference = Guid.NewGuid().ToString(),
        });
    }
}
