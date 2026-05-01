using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Data.Context;
using WebApplication3.Models.Dto;
using WebApplication3.Models.Entities;

namespace WebApplication3.Controllers.Customers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly BankManagementSystemContext _context;

    public CustomersController(BankManagementSystemContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var customers = await _context.Customers
            .AsNoTracking()
            .Where(c => !c.IsDeleted)
            .ToListAsync();
        return Ok(customers);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var customer = await _context.Customers
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.CustomerId == id && !c.IsDeleted);
        if (customer is null)
            return NotFound();
        return Ok(customer);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCustomerDto dto)
    {
        var customer = new Customer
        {
            CustomerId = Guid.NewGuid(),
            FullName = dto.FullName,
            Email = dto.Email,
            Phone = dto.Phone,
            Address = dto.Address,
            BirthDate = dto.BirthDate,
            Gender = dto.Gender,
            NationalNumber = dto.NationalNumber,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = dto.CreatedBy,
            IsDeleted = false
        };
        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = customer.CustomerId }, customer);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCustomerDto dto)
    {
        var customer = await _context.Customers
            .FirstOrDefaultAsync(c => c.CustomerId == id && !c.IsDeleted);
        if (customer is null)
            return NotFound();

        if (dto.FullName is not null)
            customer.FullName = dto.FullName;
        if (dto.Email is not null)
            customer.Email = dto.Email;
        if (dto.Phone is not null)
            customer.Phone = dto.Phone;
        if (dto.Address is not null)
            customer.Address = dto.Address;
        if (dto.BirthDate is not null)
            customer.BirthDate = dto.BirthDate;
        if (dto.Gender is not null)
            customer.Gender = dto.Gender;
        if (dto.NationalNumber is not null)
            customer.NationalNumber = dto.NationalNumber;

        customer.UpdatedAt = DateTime.UtcNow;
        customer.UpdatedBy = dto.UpdatedBy;

        await _context.SaveChangesAsync();
        return Ok(customer);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var customer = await _context.Customers
            .FirstOrDefaultAsync(c => c.CustomerId == id && !c.IsDeleted);
        if (customer is null)
            return NotFound();

        customer.IsDeleted = true;
        customer.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();
        return NoContent();
    }
}