using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Data.Context;
using WebApplication3.Models.Dto;
using WebApplication3.Models.Entities;

namespace WebApplication3.Controllers.Transactions;

[ApiController]
[Route("api/[controller]")]
public class TransactionsController : ControllerBase
{
    private readonly BankManagementSystemContext _context;

    public TransactionsController(BankManagementSystemContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var transactions = await _context.Transactions
            .AsNoTracking()
            .Where(t => !t.IsDeleted)
            .ToListAsync();
        return Ok(transactions);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var transaction = await _context.Transactions
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.TransactionId == id && !t.IsDeleted);
        if (transaction is null)
            return NotFound();
        return Ok(transaction);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTransactionDto dto)
    {
        var transaction = new Transaction
        {
            TransactionId = Guid.NewGuid(),
            TransactionDate = dto.TransactionDate,
            Amount = dto.Amount,
            TransactionType = dto.TransactionType,
            Note = dto.Note,
            AccountId = dto.AccountId,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = dto.CreatedBy,
            IsDeleted = false
        };
        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = transaction.TransactionId }, transaction);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTransactionDto dto)
    {
        var transaction = await _context.Transactions
            .FirstOrDefaultAsync(t => t.TransactionId == id && !t.IsDeleted);
        if (transaction is null)
            return NotFound();

        if (dto.TransactionDate is not null)
            transaction.TransactionDate = dto.TransactionDate.Value;
        if (dto.Amount is not null)
            transaction.Amount = dto.Amount.Value;
        if (dto.TransactionType is not null)
            transaction.TransactionType = dto.TransactionType;
        if (dto.Note is not null)
            transaction.Note = dto.Note;

        transaction.UpdatedAt = DateTime.UtcNow;
        transaction.UpdatedBy = dto.UpdatedBy;

        await _context.SaveChangesAsync();
        return Ok(transaction);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var transaction = await _context.Transactions
            .FirstOrDefaultAsync(t => t.TransactionId == id && !t.IsDeleted);
        if (transaction is null)
            return NotFound();

        transaction.IsDeleted = true;
        transaction.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();
        return NoContent();
    }
}