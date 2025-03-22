using BookManager.Domain.Entity;
using BookManager.Domain.Interface.Repositories;
using BookManager.Domain.Interface.Services;

namespace BookManager.Business.Services;

public class UserBookService(IUserBookRepository userBookRepository) : IUserBookService
{
    private readonly IUserBookRepository _userBookRepository = userBookRepository;

    public async Task<UserBook> CreateAsync(UserBook model)
    {
        return await _userBookRepository.CreateAsync(model);
    }

    public Task<IEnumerable<UserBook>> GetAllAsync()
    {
        return Task.FromResult(_userBookRepository
            .Query(b => b.Id != Guid.Empty)
            .AsEnumerable());
    }

    public async Task<UserBook> GetByIdAsync(Guid userBookId)
    {
        return await _userBookRepository.GetAsync(u => u.Id == userBookId) ?? new UserBook();
    }

    public async Task<UserBook> UpdateAsync(UserBook model)
    {
        var updated =  await _userBookRepository.UpdateAsync(model);

        return updated ? await _userBookRepository.GetAsync(u => u.Id == model.Id) ?? new UserBook() : new UserBook();
    }

    public async Task<bool> DeleteByIdAsync(Guid id)
    {
        return await _userBookRepository.DeleteAsync(u => u.Id == id);
    }
}
