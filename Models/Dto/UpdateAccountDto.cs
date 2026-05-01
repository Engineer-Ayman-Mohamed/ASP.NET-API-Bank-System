namespace WebApplication3.Models.Dto;

public record UpdateAccountDto(
    string? AccountType,
    decimal? Balance,
    Guid? BranchId,
    string? UpdatedBy
);