using System;
using System.Collections.Generic;

namespace WebApplication3.Models.Entities;

public partial class CustomerAccount
{
    public Guid CustomerAccountId { get; set; }

    public Guid CustomerId { get; set; }

    public Guid AccountNumber { get; set; }

    public DateTime OwnerShipStartDate { get; set; }

    public string AccountStatus { get; set; } = null!;

    public string OwnerShipType { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public virtual Account AccountNumberNavigation { get; set; } = null!;

    public virtual Customer Customer { get; set; } = null!;
}
