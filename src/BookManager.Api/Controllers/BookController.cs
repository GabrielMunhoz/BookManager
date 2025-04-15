using BookManager.Domain.Entity;
using BookManager.Domain.Interface.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookController(ILogger<BookController> logger, IBookService bookService) : ControllerBase
{
    private readonly ILogger<BookController> _logger = logger;
    private readonly IBookService _bookService = bookService;

    [HttpPost(Name = "BookCreateAsync")]
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(Book), 200)]
    public async Task<IActionResult> CreateAsync(Book model)
    {
        _logger.LogInformation("Invoked CreateAsync method"); 

        return Ok(await _bookService.CreateAsync(model));
    }

    [HttpGet(Name = "BookGetAllsAsync")]
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(IEnumerable<Book>), 200)]
    public async Task<IActionResult> GetAllAsync()
    {
        _logger.LogInformation("Invoked GetAllAsync method"); 

        return Ok(await _bookService.GetAllAsync());
    } 
    
    [HttpGet("{bookId}", Name = "BookGetByIdAsync")]
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(Book), 200)]
    public async Task<IActionResult> GetBookByIdAsync(Guid bookId)
    {
        _logger.LogInformation("Invoked GetByIdAsync method"); 

        return Ok(await _bookService.GetByIdAsync(bookId));
    }

    [HttpPut(Name = "BookUpdateAsync")]
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(Book), 200)]
    public async Task<IActionResult> UpdateAsync(Book model)
    {
        _logger.LogInformation("Invoked UpdateAsync method");
        
        return Ok(await _bookService.UpdateAsync(model));
    }

    [HttpDelete("{bookId}", Name = "BookDeleteByIdAsync")]
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(bool), 200)]
    public async Task<IActionResult> DeleteByIdAsync(Guid bookId)
    {
        _logger.LogInformation("Invoked DeleteByIdAsync method");

        return Ok(await _bookService.DeleteByIdAsync(bookId));
    }
}
