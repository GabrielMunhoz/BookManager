using BookManager.Domain.Commom.Enums;
using BookManager.Domain.Commom.Filter;

namespace BookManager.Domain.Model.Loans;
public class LoanFilterRequest : PagedFilter
{
    public string? UserName { get; set; }
    public string? UserEmail { get; set; }
    public string? BookTitle { get; set; }
    public List<LoanStatus> StatusLoan { get; set; } = [];
    public DateTime? InitialReturnDate { get; set; }
    public DateTime? FinalReturnDate { get; set; }
    public DateTime? InitialCreateDate { get; set; }
    public DateTime? FinalCreateDate { get; set; }
}
