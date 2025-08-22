using BookManager.Business.Strategy.Loans.Interface;
using BookManager.Domain.Entity;
using BookManager.Domain.Model.Loans;

namespace BookManager.Business.Strategy.Loans.ReturnBookCalculation;

public class InterestStrategy : IReturnBookCalculationStrategy
{
    public RequestReturnBook Calculate(Loan loan)
    {
        decimal value = loan.TotalValue;
        decimal percent = 5m;
        return new RequestReturnBook
        {
            Message = "The return date is less than today, so you can apply an interest",
            PercentInterestOrdiscount = percent,
            Value = value,
            ValueCalculated = value + (value * (percent / 100))
        };
    }
}
