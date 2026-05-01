using System;
using System.Collections.Generic;

namespace WebApplication3.Models.Entities;

public partial class Manager
{
    public Guid Id { get; set; }

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public DateTime HireDate { get; set; }

    public Guid BranchId { get; set; }

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public virtual Branch Branch { get; set; } = null!;
}
