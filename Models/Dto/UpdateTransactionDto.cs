namespace WebApplication3.Models.Dto;

public record UpdateTransactionDto(
    DateTime? TransactionDate,
    decimal? Amount,
    string? TransactionType,
    string? Note,
    string? UpdatedBy
);