using BookManager.Domain.Commom.Enums;
using BookManager.Domain.Commom.Results;
using BookManager.Domain.Interface.Common;
using BookManager.Domain.Interface.Services;
using BookManager.Domain.Model.Loans;
using Microsoft.AspNetCore.Mvc;

namespace BookManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoanController(ILoanService loanService, 
    ILogger<LoanController> logger,
    INotifier notifier) : ControllerBase
{
    private readonly ILoanService _loanService = loanService;
    private readonly ILogger<LoanController> _logger = logger;
    private readonly INotifier _notifier = notifier;

    [HttpPost("LoanCreateAsync")]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateAsync(LoanRequest model)
    {
        _notifier.AddNotification(Issues.i001, "Invoked CreateAsync method");

        return Ok(await _loanService.CreateAsync(model));
    }

    [HttpGet("LoanGetAllAsync")]
    [ProducesResponseType(typeof(Result<IEnumerable<LoanResponseList>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        _notifier.AddNotification(Issues.i002, "Invoked GetAllAsync method");
        return Ok(await _loanService.GetAllAsync());
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
