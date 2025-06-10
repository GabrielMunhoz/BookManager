using BookManager.Business.Strategy.Loans.Interface;
using BookManager.Domain.Entity;
using BookManager.Domain.Model.Loans;

namespace BookManager.Business.Strategy.Loans.ReturnBookCalculation;

public class NoChangeStrategy : IReturnBookCalculationStrategy
{
    public RequestReturnBook Calculate(Loan loan)
    {
        decimal value = 10m;
        return new RequestReturnBook
        {
            Message = "The return date is today",
            PercentInterestOrdiscount = 0m,
            Value = value,
            ValueCalculated = 0
        };
    }
}
