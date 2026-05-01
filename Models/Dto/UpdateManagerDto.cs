namespace WebApplication3.Models.Dto;

public record UpdateManagerDto(
    string? FullName,
    string? Email,
    string? PhoneNumber,
    DateTime? HireDate,
    Guid? BranchId,
    string? UpdatedBy
);