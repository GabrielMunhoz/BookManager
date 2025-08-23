using BookManager.Domain.Entity;

namespace BookManager.Domain.Model.User;
public class UsersCreate
{
    public string Name { get; set; }
    public string Email { get; set; }
    public Address Address { get; set; }
}
