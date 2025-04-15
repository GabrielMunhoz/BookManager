using BookManager.Domain.Entity;
using BookManager.Domain.Interface.Repositories;
using BookManager.Domain.Interface.Services;

namespace BookManager.Business.Services;

public class UserService(IUserRepository userRepository) : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<Users> CreateAsync(Users model)
    {
        return await _userRepository.CreateAsync(model);
    }

    public Task<IEnumerable<Users>> GetAllAsync()
    {
        return Task.FromResult(_userRepository
            .Query(b => b.Id != Guid.Empty)
            .AsEnumerable());
    }

    public async Task<Users> GetByIdAsync(Guid userId)
    {
        return await _userRepository.GetAsync(u => u.Id == userId) ?? new Users();
    }

    public async Task<Users> UpdateAsync(Users model)
    {
        var updated =  await _userRepository.UpdateAsync(model);

        return updated ? await _userRepository.GetAsync(u => u.Id == model.Id) ?? new Users() : new Users();
    }

    public async Task<bool> DeleteByIdAsync(Guid id)
    {
        return await _userRepository.DeleteAsync(u => u.Id == id);
    }
}
