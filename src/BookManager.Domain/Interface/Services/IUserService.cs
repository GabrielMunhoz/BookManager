using BookManager.Domain.Commom.Results;
using BookManager.Domain.Model.User;

namespace BookManager.Domain.Interface.Services;
public interface IUserService
{
    Task<Result<UsersDetail>> GetByIdAsync(Guid bookId);
    Task<Result<IEnumerable<UsersList>>> GetAllAsync();
    Task<Result<bool>> CreateAsync(UsersCreate usersCreate);
    Task<Result<bool>> UpdateAsync(UsersUpdate usersUpdate);
    Task<Result<bool>> DeleteByIdAsync(Guid id);
}
