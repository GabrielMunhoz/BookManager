using AutoMapper;
using BookManager.Domain.Commom.Enums;
using BookManager.Domain.Commom.Results;
using BookManager.Domain.Entity;
using BookManager.Domain.Extensions;
using BookManager.Domain.Interface.Common;
using BookManager.Domain.Interface.Repositories;
using BookManager.Domain.Interface.Services;
using BookManager.Domain.Model.User;
using FluentValidation;

namespace BookManager.Business.Services;

public class UserService(IUserRepository _userRepository, 
    IValidator<Users> _validator,  
    IMapper _mapper,
    INotifier _notifier) : IUserService
{
    public async Task<Result<bool>> CreateAsync(UsersCreate usersCreate)
    {
        _notifier.AddNotification(Issues.i1009, "Invoked CreateAsync method from UserService");

        var user = _mapper.Map<Users>(usersCreate);

        var validatorResult = _validator.Validate(user);

        if (!validatorResult.IsValid)
            return validatorResult.ToFailureResult<bool>();

        var result = await _userRepository.CreateAsync(user);

        if (result?.Id == Guid.Empty)
            return Result.Failure<bool>(new Error(Issues.e1010, "Create users failed"));

        return Result.Success(true);
    }

    public Task<Result<IEnumerable<UsersList>>> GetAllAsync()
    {
        _notifier.AddNotification(Issues.i1010, "Invoked GetAllAsync method from UserService");

        var result = _userRepository
            .Query(b => b.Id != Guid.Empty)
            .AsEnumerable() ?? []; 

        var mapped = _mapper.Map<IEnumerable<UsersList>>(result);

        return Task.FromResult(Result.Success(mapped));
    }

    public async Task<Result<UsersDetail>> GetByIdAsync(Guid userId)
    {
        _notifier.AddNotification(Issues.i1010, "Invoked GetAllAsync method from UserService");

        var result = await _userRepository.GetAsync(u => u.Id == userId) ?? new Users(); 
        if(result?.Id == Guid.Empty)
            return Result.Failure<UsersDetail>(new Error(Issues.e1011, "User not found"));

        var mapped = _mapper.Map<UsersDetail>(result);

        return Result.Success(mapped);
    }

    public async Task<Result<bool>> UpdateAsync(UsersUpdate usersUpdate)
    {
        _notifier.AddNotification(Issues.i1011, "Invoked UpdateAsync method from UserService");

        var user = _mapper.Map<Users>(usersUpdate);

        var validatorResult = _validator.Validate(user);
        if (!validatorResult.IsValid)
            return validatorResult.ToFailureResult<bool>();

        var updated =  await _userRepository.UpdateAsync(user);
        if (!updated)
            return Result.Failure<bool>(new Error(Issues.e1012, "Update user failed"));

        return Result.Success(true);
    }

    public async Task<Result<bool>> DeleteByIdAsync(Guid id)
    {
        _notifier.AddNotification(Issues.i1012, "Invoked DeleteByIdAsync method from UserService");

        var deleted = await _userRepository.DeleteAsync(u => u.Id == id);
        if (!deleted)
            return Result.Failure<bool>(new Error(Issues.e1013, "Delete user failed"));

        return Result.Success(true);
    }
}
