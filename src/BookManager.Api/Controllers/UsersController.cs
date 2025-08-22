using BookManager.Domain.Commom.Enums;
using BookManager.Domain.Commom.Results;
using BookManager.Domain.Interface.Common;
using BookManager.Domain.Interface.Services;
using BookManager.Domain.Model.User;
using Microsoft.AspNetCore.Mvc;

namespace BookManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController(INotifier _notifier, IUserService _userService) : ControllerBase
{
    [HttpPost("CreateAsync")]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateAsync(UsersCreate usersCreate)
    {
        _notifier.AddNotification(Issues.i010, "Invoked CreateAsync method"); 

        return Ok(await _userService.CreateAsync(usersCreate));
    }

    [HttpGet("GetAllAsync")]
    [ProducesResponseType(typeof(Result<IEnumerable<UsersList>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllAsync()
    {
        _notifier.AddNotification(Issues.i011, "Invoked GetAllAsync method");

        return Ok(await _userService.GetAllAsync());
    } 
    
    [HttpGet("GetByIdAsync/{userId}")]
    [ProducesResponseType(typeof(Result<UsersDetail>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUserByIdAsync(Guid userId)
    {
        _notifier.AddNotification(Issues.i012, "Invoked GetUserByIdAsync method");

        return Ok(await _userService.GetByIdAsync(userId));
    }

    [HttpPatch("UpdateAsync")]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateAsync(UsersUpdate usersUpdate)
    {
        _notifier.AddNotification(Issues.i013, "Invoked UpdateAsync method");

        return Ok(await _userService.UpdateAsync(usersUpdate));
    }

    [HttpDelete("DeleteByIdAsync/{userId}")]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteByIdAsync(Guid userId)
    {
        _notifier.AddNotification(Issues.i014, "Invoked DeleteByIdAsync method");

        return Ok(await _userService.DeleteByIdAsync(userId));
    }
}
