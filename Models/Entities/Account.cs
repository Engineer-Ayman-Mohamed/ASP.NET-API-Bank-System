using System;
using System.Collections.Generic;

namespace WebApplication3.Models.Entities;

public partial class Account
{
    public Guid AccountNumber { get; set; }

    public decimal Balance { get; set; }

    public string AccountType { get; set; } = null!;

    public DateTime OpenDate { get; set; }

    public Guid BranchId { get; set; }

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public virtual Branch Branch { get; set; } = null!;

    public virtual ICollection<CustomerAccount> CustomerAccounts { get; set; } = new List<CustomerAccount>();

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
