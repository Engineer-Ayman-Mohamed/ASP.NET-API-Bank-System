namespace WebApplication3.Models.Dto;

public record UpdateBranchDto(
    string? Name,
    string? Address,
    string? PhoneNumber,
    string? UpdatedBy
);