using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Data.Context;
using WebApplication3.Models.Dto;
using WebApplication3.Models.Entities;

namespace WebApplication3.Controllers.Managers;

[ApiController]
[Route("api/[controller]")]
public class ManagersController : ControllerBase
{
    private readonly BankManagementSystemContext _context;

    public ManagersController(BankManagementSystemContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var managers = await _context.Managers
            .AsNoTracking()
            .Where(m => !m.IsDeleted)
            .ToListAsync();
        return Ok(managers);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var manager = await _context.Managers
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Id == id && !m.IsDeleted);
        if (manager is null)
            return NotFound();
        return Ok(manager);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateManagerDto dto)
    {
        var manager = new Manager
        {
            Id = Guid.NewGuid(),
            FullName = dto.FullName,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber,
            HireDate = dto.HireDate,
            BranchId = dto.BranchId,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = dto.CreatedBy,
            IsDeleted = false
        };
        _context.Managers.Add(manager);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = manager.Id }, manager);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateManagerDto dto)
    {
        var manager = await _context.Managers
            .FirstOrDefaultAsync(m => m.Id == id && !m.IsDeleted);
        if (manager is null)
            return NotFound();

        if (dto.FullName is not null)
            manager.FullName = dto.FullName;
        if (dto.Email is not null)
            manager.Email = dto.Email;
        if (dto.PhoneNumber is not null)
            manager.PhoneNumber = dto.PhoneNumber;
        if (dto.HireDate is not null)
            manager.HireDate = dto.HireDate.Value;
        if (dto.BranchId is not null)
            manager.BranchId = dto.BranchId.Value;

        manager.UpdatedAt = DateTime.UtcNow;
        manager.UpdatedBy = dto.UpdatedBy;

        await _context.SaveChangesAsync();
        return Ok(manager);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var manager = await _context.Managers
            .FirstOrDefaultAsync(m => m.Id == id && !m.IsDeleted);
        if (manager is null)
            return NotFound();

        manager.IsDeleted = true;
        manager.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();
        return NoContent();
    }
}