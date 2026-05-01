using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Models.Entities;

namespace WebApplication3.Data.Context;

public partial class BankManagementSystemContext : DbContext
{
    public BankManagementSystemContext() { }
    public BankManagementSystemContext(DbContextOptions<BankManagementSystemContext> options)
        : base(options) { }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Branch> Branches { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<CustomerAccount> CustomerAccounts { get; set; }

    public virtual DbSet<Manager> Managers { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.AccountNumber);

            entity.ToTable("Accounts", "Bank");

            entity.HasIndex(e => e.BranchId, "IX_Accounts_BranchId");

            entity.Property(e => e.AccountNumber).ValueGeneratedNever();
            entity.Property(e => e.AccountType)
                .HasMaxLength(20)
                .HasDefaultValue("Savings");
            entity.Property(e => e.Balance).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CreatedBy).HasDefaultValue("");
            entity.Property(e => e.OpenDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Branch).WithMany(p => p.Accounts).HasForeignKey(d => d.BranchId);
        });

        modelBuilder.Entity<Branch>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Branch");

            entity.ToTable("Branches", "Bank");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Address).HasMaxLength(50);
            entity.Property(e => e.CreatedBy).HasDefaultValue("");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.PhoneNumber).HasMaxLength(50);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK_Customer");

            entity.ToTable("Customers", "Bank");

            entity.Property(e => e.CustomerId).ValueGeneratedNever();
            entity.Property(e => e.Address).HasMaxLength(200);
            entity.Property(e => e.BirthDate).HasColumnType("datetime");
            entity.Property(e => e.CreatedBy).HasDefaultValue("");
            entity.Property(e => e.Email).HasMaxLength(200);
            entity.Property(e => e.FullName).HasMaxLength(200);
            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.Property(e => e.NationalNumber).HasMaxLength(50);
            entity.Property(e => e.Phone).HasMaxLength(50);
        });

        modelBuilder.Entity<CustomerAccount>(entity =>
        {
            entity.HasKey(e => new { e.CustomerAccountId, e.CustomerId });

            entity.ToTable("CustomerAccounts", "Bank");

            entity.HasIndex(e => e.AccountNumber, "IX_CustomerAccounts_AccountNumber");

            entity.HasIndex(e => e.CustomerId, "IX_CustomerAccounts_CustomerId");

            entity.Property(e => e.AccountStatus).HasMaxLength(50);
            entity.Property(e => e.CreatedBy).HasDefaultValue("");
            entity.Property(e => e.OwnerShipStartDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.OwnerShipType).HasMaxLength(50);

            entity.HasOne(d => d.AccountNumberNavigation).WithMany(p => p.CustomerAccounts).HasForeignKey(d => d.AccountNumber);

            entity.HasOne(d => d.Customer).WithMany(p => p.CustomerAccounts).HasForeignKey(d => d.CustomerId);
        });

        modelBuilder.Entity<Manager>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Manager");

            entity.ToTable("Managers", "Bank");

            entity.HasIndex(e => e.BranchId, "IX_Managers_BranchId").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreatedBy).HasDefaultValue("");
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.HireDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.PhoneNumber).HasMaxLength(50);

            entity.HasOne(d => d.Branch).WithOne(p => p.Manager).HasForeignKey<Manager>(d => d.BranchId);
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.ToTable("Transactions", "Bank");

            entity.HasIndex(e => e.AccountId, "IX_Transactions_AccountId");

            entity.Property(e => e.TransactionId).ValueGeneratedNever();
            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CreatedBy).HasDefaultValue("");
            entity.Property(e => e.TransactionDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.TransactionType)
                .HasMaxLength(50)
                .HasDefaultValue("Deposit");

            entity.HasOne(d => d.Account).WithMany(p => p.Transactions).HasForeignKey(d => d.AccountId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
