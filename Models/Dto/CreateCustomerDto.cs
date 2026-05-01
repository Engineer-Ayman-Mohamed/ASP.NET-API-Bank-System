namespace WebApplication3.Models.Dto;

public record CreateCustomerDto(
    string FullName,
    string? Email,
    string Phone,
    string Address,
    DateTime? BirthDate,
    string Gender,
    string NationalNumber,
    string CreatedBy
);