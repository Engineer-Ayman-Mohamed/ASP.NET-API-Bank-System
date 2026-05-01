namespace WebApplication3.Models.Dto;

public record CreateAccountDto(
    decimal Balance,
    string AccountType,
    DateTime OpenDate,
    Guid BranchId,
    string CreatedBy
);