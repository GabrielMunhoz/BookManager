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
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(Result<bool>), 200)]
    public async Task<IActionResult> CreateAsync(LoanRequest model)
    {
        _logger.LogInformation("Invoked CreateAsync method");

        return Ok(await _loanService.CreateAsync(model));
    }

    [HttpGet("LoanGetAll")]
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(Result<IEnumerable<LoanResponseList>>), 200)]
    public async Task<IActionResult> GetAll()
    {
        _logger.LogInformation("Invoked GetAllAsync method");
        _notifier.AddError("Teste", "Erro no teste");
        _notifier.AddError("Teste", "Erro no teste");
        return Ok(await _loanService.GetAllAsync());
    }

    [HttpPatch("RequestReturnBookAsync")]
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(Result<RequestReturnBook>), 200)]
    public async Task<IActionResult> RequestReturnBookAsync(Guid id)
    {
        _logger.LogInformation("Invoked RequestReturnBookAsync method");

        return Ok(await _loanService.RequestReturnBookAsync(id));
    }

    [HttpPatch("Book/ReturnBookAsync")]
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(Result<RequestReturnBook>), 200)]
    public async Task<IActionResult> ReturnBookAsync(ReturnBookRequest returnBookRequest)
    {
        _logger.LogInformation("Invoked ReturnBookAsync method");

        return Ok(await _loanService.ReturnBookAsync(returnBookRequest));
    }
}
