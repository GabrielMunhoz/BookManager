using BookManager.Domain.Entity;
using BookManager.Domain.Interface.Repositories;
using BookManager.Domain.Interface.Services;
using FluentValidation;

namespace BookManager.Business.Services;

public class UserService(IUserRepository userRepository, IValidator<Users> validator) : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IValidator<Users> _validator = validator;

    public async Task<Users> CreateAsync(Users model)
    {
        return await _userRepository.CreateAsync(model);
    }

    public async Task<IEnumerable<Users>> GetAllAsync()
    {
        return _userRepository
            .Query(b => b.Id != Guid.Empty)
            .AsEnumerable();
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
