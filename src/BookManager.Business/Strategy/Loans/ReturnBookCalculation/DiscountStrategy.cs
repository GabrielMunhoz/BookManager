using BookManager.Business.Strategy.Loans.Interface;
using BookManager.Domain.Entity;
using BookManager.Domain.Model.Loans;

namespace BookManager.Business.Strategy.Loans.ReturnBookCalculation;

public class DiscountStrategy : IReturnBookCalculationStrategy
{
    public RequestReturnBook Calculate(Loan loan)
    {
        decimal value = 10m;
        decimal percent = 5m;
        return new RequestReturnBook
        {
            Message = "The return date is greater than today, so you can apply a discount",
            PercentInterestOrdiscount = percent,
            Value = value,
            ValueCalculated = value - (value * (percent / 100))
        };
    }
}
