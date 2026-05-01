using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Data.Context;
using WebApplication3.Models.Dto;
using WebApplication3.Models.Entities;

namespace WebApplication3.Controllers.CustomerAccounts;

[ApiController]
[Route("api/[controller]")]
public class CustomerAccountsController : ControllerBase
{
    private readonly BankManagementSystemContext _context;

    public CustomerAccountsController(BankManagementSystemContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var customerAccounts = await _context.CustomerAccounts
            .AsNoTracking()
            .Where(ca => !ca.IsDeleted)
            .ToListAsync();
        return Ok(customerAccounts);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var customerAccount = await _context.CustomerAccounts
            .AsNoTracking()
            .FirstOrDefaultAsync(ca => ca.CustomerAccountId == id && !ca.IsDeleted);
        if (customerAccount is null)
            return NotFound();
        return Ok(customerAccount);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCustomerAccountDto dto)
    {
        var customerAccount = new CustomerAccount
        {
            CustomerAccountId = Guid.NewGuid(),
            CustomerId = dto.CustomerId,
            AccountNumber = dto.AccountNumber,
            OwnerShipStartDate = dto.OwnerShipStartDate,
            AccountStatus = dto.AccountStatus,
            OwnerShipType = dto.OwnerShipType,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = dto.CreatedBy,
            IsDeleted = false
        };
        _context.CustomerAccounts.Add(customerAccount);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = customerAccount.CustomerAccountId }, customerAccount);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCustomerAccountDto dto)
    {
        var customerAccount = await _context.CustomerAccounts
            .FirstOrDefaultAsync(ca => ca.CustomerAccountId == id && !ca.IsDeleted);
        if (customerAccount is null)
            return NotFound();

        if (dto.OwnerShipStartDate is not null)
            customerAccount.OwnerShipStartDate = dto.OwnerShipStartDate.Value;
        if (dto.AccountStatus is not null)
            customerAccount.AccountStatus = dto.AccountStatus;
        if (dto.OwnerShipType is not null)
            customerAccount.OwnerShipType = dto.OwnerShipType;

        customerAccount.UpdatedAt = DateTime.UtcNow;
        customerAccount.UpdatedBy = dto.UpdatedBy;

        await _context.SaveChangesAsync();
        return Ok(customerAccount);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var customerAccount = await _context.CustomerAccounts
            .FirstOrDefaultAsync(ca => ca.CustomerAccountId == id && !ca.IsDeleted);
        if (customerAccount is null)
            return NotFound();

        customerAccount.IsDeleted = true;
        customerAccount.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();
        return NoContent();
    }
}