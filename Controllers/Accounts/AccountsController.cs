using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Data.Context;
using WebApplication3.Models.Dto;
using WebApplication3.Models.Entities;

namespace WebApplication3.Controllers.Accounts;

[ApiController]
[Route("api/[controller]")]
public class AccountsController : ControllerBase
{
    private readonly BankManagementSystemContext _context;

    public AccountsController(BankManagementSystemContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var accounts = await _context.Accounts
            .AsNoTracking()
            .Where(a => !a.IsDeleted)
            .ToListAsync();
        return Ok(accounts);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var account = await _context.Accounts
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.AccountNumber == id && !a.IsDeleted);
        if (account is null)
            return NotFound();
        return Ok(account);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAccountDto dto)
    {
        var account = new Account
        {
            AccountNumber = Guid.NewGuid(),
            Balance = dto.Balance,
            AccountType = dto.AccountType,
            OpenDate = dto.OpenDate,
            BranchId = dto.BranchId,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = dto.CreatedBy,
            IsDeleted = false
        };
        _context.Accounts.Add(account);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = account.AccountNumber }, account);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateAccountDto dto)
    {
        var account = await _context.Accounts
            .FirstOrDefaultAsync(a => a.AccountNumber == id && !a.IsDeleted);
        if (account is null)
            return NotFound();

        if (dto.AccountType is not null)
            account.AccountType = dto.AccountType;
        if (dto.Balance is not null)
            account.Balance = dto.Balance.Value;
        if (dto.BranchId is not null)
            account.BranchId = dto.BranchId.Value;

        account.UpdatedAt = DateTime.UtcNow;
        account.UpdatedBy = dto.UpdatedBy;

        await _context.SaveChangesAsync();
        return Ok(account);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var account = await _context.Accounts
            .FirstOrDefaultAsync(a => a.AccountNumber == id && !a.IsDeleted);
        if (account is null)
            return NotFound();

        account.IsDeleted = true;
        account.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();
        return NoContent();
    }
}