using BookManager.Domain.Commom.Enums;
using BookManager.Domain.Commom.Results;
using BookManager.Domain.Interface.Common;
using BookManager.Domain.Interface.Services;
using BookManager.Domain.Model.Books;
using Microsoft.AspNetCore.Mvc;

namespace BookManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookController(INotifier notifier, IBookService bookService) : ControllerBase
{
    private readonly INotifier _notifier = notifier;
    private readonly IBookService _bookService = bookService;

    [HttpPost("CreateAsync")]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateAsync(BookCreate bookCreate)
    {
        _notifier.AddNotification(Issues.i005 , "Invoked CreateAsync method"); 

        return Ok(await _bookService.CreateAsync(bookCreate));
    }

    [HttpGet("GetAllAsync")]
    [ProducesResponseType(typeof(Result<IEnumerable<BookList>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllAsync()
    {
        _notifier.AddNotification(Issues.i006, "Invoked GetAllAsync method");

        return Ok(await _bookService.GetAllAsync());
    } 
    
    [HttpGet("GetByIdAsync/{bookId}")]
    [ProducesResponseType(typeof(Result<BookDetail>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBookByIdAsync(Guid bookId)
    {
        _notifier.AddNotification(Issues.i007, "Invoked GetBookByIdAsync method");

        return Ok(await _bookService.GetByIdAsync(bookId));
    }

    [HttpPatch("UpdateAsync")]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateAsync(BookUpdate bookUpdate)
    {
        _notifier.AddNotification(Issues.i008, "Invoked UpdateAsync method");

        return Ok(await _bookService.UpdateAsync(bookUpdate));
    }

    [HttpDelete("DeleteByIdAsync/{bookId}")]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteByIdAsync(Guid bookId)
    {
        _notifier.AddNotification(Issues.i009, "Invoked DeleteByIdAsync method");

        return Ok(await _bookService.DeleteByIdAsync(bookId));
    }
}
