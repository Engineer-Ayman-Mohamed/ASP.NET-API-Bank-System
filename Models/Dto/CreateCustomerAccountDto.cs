namespace WebApplication3.Models.Dto;

public record CreateCustomerAccountDto(
    Guid CustomerId,
    Guid AccountNumber,
    DateTime OwnerShipStartDate,
    string AccountStatus,
    string OwnerShipType,
    string CreatedBy
);