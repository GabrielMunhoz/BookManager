using BookManager.Domain.Entity;

namespace BookManager.Domain.Model.User;
public class UsersDetail
{
    public Guid Id { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime? UpdateDate { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }

    public Address Address { get; set; }
}
