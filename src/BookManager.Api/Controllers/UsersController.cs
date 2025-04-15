using BookManager.Domain.Entity;
using BookManager.Domain.Interface.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController(ILogger<UsersController> logger, IUserService userService) : ControllerBase
{
    private readonly ILogger<UsersController> _logger = logger;
    private readonly IUserService _userService = userService;

    [HttpPost(Name = "UserCreateAsync")]
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(Users), 200)]
    public async Task<IActionResult> CreateAsync(Users model)
    {
        _logger.LogInformation("Invoked CreateAsync method"); 

        return Ok(await _userService.CreateAsync(model));
    }

    [HttpGet(Name = "UserGetAllAsync")]
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(IEnumerable<Users>), 200)]
    public async Task<IActionResult> GetAllAsync()
    {
        _logger.LogInformation("Invoked GetAllAsync method"); 

        return Ok(await _userService.GetAllAsync());
    } 
    
    [HttpGet("{userId}" ,Name = "UserGetByIdAsync")]
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(Users), 200)]
    public async Task<IActionResult> GetUserByIdAsync(Guid userId)
    {
        _logger.LogInformation("Invoked GetByIdAsync method"); 

        return Ok(await _userService.GetByIdAsync(userId));
    }

    [HttpPut(Name = "UserUpdateAsync")]
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(Users), 200)]
    public async Task<IActionResult> UpdateAsync(Users model)
    {
        _logger.LogInformation("Invoked UpdateAsync method");
        
        return Ok(await _userService.UpdateAsync(model));
    }

    [HttpDelete("{userId}", Name = "UserDeleteByIdAsync")]
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(bool), 200)]
    public async Task<IActionResult> DeleteByIdAsync(Guid userId)
    {
        _logger.LogInformation("Invoked DeleteByIdAsync method");

        return Ok(await _userService.DeleteByIdAsync(userId));
    }
}
