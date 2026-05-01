namespace WebApplication3.Models.Dto;

public record UpdateCustomerAccountDto(
    DateTime? OwnerShipStartDate,
    string? AccountStatus,
    string? OwnerShipType,
    string? UpdatedBy
);