using BookManager.Domain.Entity;
using BookManager.Domain.Interface.Services;
using BookManager.Domain.Model.Loans;
using Microsoft.AspNetCore.Mvc;

namespace BookManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoanController(ILoanService loanService, ILogger<LoanController> logger) : ControllerBase
{
    private readonly ILoanService _loanService = loanService;
    private readonly ILogger<LoanController> _logger = logger;

    [HttpPost(Name = "LoanCreateAsync")]
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(Loan), 200)]
    public async Task<IActionResult> CreateAsync(LoanRequest model)
    {
        _logger.LogInformation("Invoked CreateAsync method");

        return Ok(await _loanService.CreateAsync(model));
    }

    [HttpGet(Name = "LoanGetAll")]
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(IEnumerable<Loan>), 200)]
    public IActionResult GetAll()
    {
        _logger.LogInformation("Invoked GetAllAsync method");

        return Ok(_loanService.GetAll());
    }
}
