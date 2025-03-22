using BookManager.Domain.Entity;
using BookManager.Domain.Interface.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserBookController(ILogger<UserBookController> logger, IUserBookService userBookService) : ControllerBase
{
    private readonly ILogger<UserBookController> _logger = logger;
    private readonly IUserBookService _userBookService = userBookService;

    [HttpPost(Name = "UserCreateAsync")]
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(UserBook), 200)]
    public async Task<IActionResult> CreateAsync(UserBook model)
    {
        _logger.LogInformation("Invoked CreateAsync method"); 

        return Ok(await _userBookService.CreateAsync(model));
    }

    [HttpGet(Name = "UserGetAllAsync")]
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(IEnumerable<UserBook>), 200)]
    public async Task<IActionResult> GetAllAsync()
    {
        _logger.LogInformation("Invoked GetAllAsync method"); 

        return Ok(await _userBookService.GetAllAsync());
    } 
    
    [HttpGet("{userId}" ,Name = "UserGetByIdAsync")]
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(UserBook), 200)]
    public async Task<IActionResult> GetUserByIdAsync(Guid userId)
    {
        _logger.LogInformation("Invoked GetByIdAsync method"); 

        return Ok(await _userBookService.GetByIdAsync(userId));
    }

    [HttpPut(Name = "UserUpdateAsync")]
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(UserBook), 200)]
    public async Task<IActionResult> UpdateAsync(UserBook model)
    {
        _logger.LogInformation("Invoked UpdateAsync method");
        
        return Ok(await _userBookService.UpdateAsync(model));
    }

    [HttpDelete("{userId}", Name = "UserDeleteByIdAsync")]
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(bool), 200)]
    public async Task<IActionResult> DeleteByIdAsync(Guid userId)
    {
        _logger.LogInformation("Invoked DeleteByIdAsync method");

        return Ok(await _userBookService.DeleteByIdAsync(userId));
    }
}
