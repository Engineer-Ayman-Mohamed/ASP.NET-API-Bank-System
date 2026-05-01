using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Data.Context;
using WebApplication3.Models.Dto;
using WebApplication3.Models.Entities;

namespace WebApplication3.Controllers.Branches;

[ApiController]
[Route("api/[controller]")]
public class BranchesController : ControllerBase
{
    private readonly BankManagementSystemContext _context;

    public BranchesController(BankManagementSystemContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var branches = await _context.Branches
            .AsNoTracking()
            .Where(b => !b.IsDeleted)
            .ToListAsync();
        return Ok(branches);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var branch = await _context.Branches
            .AsNoTracking()
            .FirstOrDefaultAsync(b => b.Id == id && !b.IsDeleted);
        if (branch is null)
            return NotFound();
        return Ok(branch);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateBranchDto dto)
    {
        var branch = new Branch
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Address = dto.Address,
            PhoneNumber = dto.PhoneNumber,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = dto.CreatedBy,
            IsDeleted = false
        };
        _context.Branches.Add(branch);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = branch.Id }, branch);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateBranchDto dto)
    {
        var branch = await _context.Branches
            .FirstOrDefaultAsync(b => b.Id == id && !b.IsDeleted);
        if (branch is null)
            return NotFound();

        if (dto.Name is not null)
            branch.Name = dto.Name;
        if (dto.Address is not null)
            branch.Address = dto.Address;
        if (dto.PhoneNumber is not null)
            branch.PhoneNumber = dto.PhoneNumber;

        branch.UpdatedAt = DateTime.UtcNow;
        branch.UpdatedBy = dto.UpdatedBy;

        await _context.SaveChangesAsync();
        return Ok(branch);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var branch = await _context.Branches
            .FirstOrDefaultAsync(b => b.Id == id && !b.IsDeleted);
        if (branch is null)
            return NotFound();

        branch.IsDeleted = true;
        branch.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();
        return NoContent();
    }
}