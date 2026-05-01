using System;
using System.Collections.Generic;

namespace WebApplication3.Models.Entities;

public partial class Transaction
{
    public Guid TransactionId { get; set; }

    public DateTime TransactionDate { get; set; }

    public decimal Amount { get; set; }

    public string TransactionType { get; set; } = null!;

    public string? Note { get; set; }

    public Guid AccountId { get; set; }

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public virtual Account Account { get; set; } = null!;
}
