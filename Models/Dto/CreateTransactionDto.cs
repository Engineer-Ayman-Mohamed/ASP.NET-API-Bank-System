namespace WebApplication3.Models.Dto;

public record CreateTransactionDto(
    DateTime TransactionDate,
    decimal Amount,
    string TransactionType,
    string? Note,
    Guid AccountId,
    string CreatedBy
);