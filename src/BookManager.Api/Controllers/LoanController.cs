using BookManager.Domain.Commom.Enums;
using BookManager.Domain.Commom.Results;
using BookManager.Domain.Interface.Common;
using BookManager.Domain.Interface.Services;
using BookManager.Domain.Model.Loans;
using Microsoft.AspNetCore.Mvc;

namespace BookManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoanController(ILoanService _loanService, 
    INotifier _notifier) : ControllerBase
{
    
    [HttpPost("CreateAsync")]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateAsync(LoanRequest model, CancellationToken cancellationToken)
    {
        _notifier.AddNotification(Issues.i001, "Invoked CreateAsync method");

        return Ok(await _loanService.CreateAsync(model, cancellationToken));
    }

    [HttpPost("GetAllPagedAsync")]
    [ProducesResponseType(typeof(PagedResult<LoanResponseList>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllPagedAsync(LoanFilterRequest loanFilterRequest, CancellationToken cancellationToken)
    {
        _notifier.AddNotification(Issues.i002, "Invoked GetAllPagedAsync method");
        return Ok(await _loanService.GetAllAsync(loanFilterRequest, cancellationToken));
    }

    [HttpGet("RequestReturnBookAsync")]
    [ProducesResponseType(typeof(Result<RequestReturnBook>), StatusCodes.Status200OK)]
    public async Task<IActionResult> RequestReturnBookAsync(Guid id)
    {
        _notifier.AddNotification(Issues.i003, "Invoked RequestReturnBookAsync method");

        return Ok(await _loanService.RequestReturnBookAsync(id));
    }

    [HttpPatch("Book/ReturnBookAsync")]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ReturnBookAsync(ReturnBookRequest returnBookRequest)
    {
        _notifier.AddNotification(Issues.i004, "Invoked ReturnBookAsync method");

        return Ok(await _loanService.ReturnBookAsync(returnBookRequest));
    }
}
