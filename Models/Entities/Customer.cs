using System;
using System.Collections.Generic;

namespace WebApplication3.Models.Entities;

public partial class Customer
{
    public Guid CustomerId { get; set; }

    public string FullName { get; set; } = null!;

    public string? Email { get; set; }

    public string Phone { get; set; } = null!;

    public string Address { get; set; } = null!;

    public DateTime? BirthDate { get; set; }

    public string Gender { get; set; } = null!;

    public string NationalNumber { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public virtual ICollection<CustomerAccount> CustomerAccounts { get; set; } = new List<CustomerAccount>();
}
