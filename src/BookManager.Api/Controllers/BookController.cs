using BookManager.Domain.Entity;
using BookManager.Domain.Interface.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookManager.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class BookController(ILogger<BookController> logger, IBookService bookService) : ControllerBase
{
    private readonly ILogger<BookController> _logger = logger;
    private readonly IBookService _bookService = bookService;

    [HttpPost(Name = "CreateAsync")]
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(Book), 200)]
    public async Task<IActionResult> CreateAsync(Book model)
    {
        _logger.LogInformation("Invoked CreateAsync method"); 

        return Ok(await _bookService.CreateBook(model));
    }

    [HttpGet(Name = "GetAllBooksAsync")]
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(IEnumerable<Book>), 200)]
    public async Task<IActionResult> GetAllAsync()
    {
        _logger.LogInformation("Invoked GetAllAsync method"); 

        return Ok(await _bookService.GetAllBooksAsync());
    }

    [HttpPut]
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(Book), 200)]
    public async Task<IActionResult> UpdateAsync(Book model)
    {
        _logger.LogInformation("Invoked UpdateAsync method");
        
        return Ok(await _bookService.UpdateBook(model));
    }
}
