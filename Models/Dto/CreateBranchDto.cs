namespace WebApplication3.Models.Dto;

public record CreateBranchDto(
    string Name,
    string Address,
    string PhoneNumber,
    string CreatedBy
);