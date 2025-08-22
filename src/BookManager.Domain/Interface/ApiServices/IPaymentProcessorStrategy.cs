using BookManager.Domain.Commom.Results;
using BookManager.Domain.Model.ApiPayment;

namespace BookManager.Infra.ApiServices.Interfaces;
public interface IPaymentProcessorStrategy
{
    Task<Result<ApiPaymentResponse>> ProcessPayment(ApiPaymentRequest requestPayment);
}
