using BookManager.Domain.Entity;
using BookManager.Domain.Model.Loans;

namespace BookManager.Business.Strategy.Loans.Interface;
public interface IReturnBookCalculationStrategy
{
    RequestReturnBook Calculate(Loan loan);
}
