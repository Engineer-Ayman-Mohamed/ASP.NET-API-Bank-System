namespace WebApplication3.Models.Dto;

public record CreateManagerDto(
    string FullName,
    string Email,
    string PhoneNumber,
    DateTime HireDate,
    Guid BranchId,
    string CreatedBy
);