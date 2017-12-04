using System;
using System.Collections.Generic;
using System.Text;
using BusTicketSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusTicketSystem.Data.Configuration
{
    public class BankAccountConfiguration : IEntityTypeConfiguration<BankAccount>
    {
        public void Configure(EntityTypeBuilder<BankAccount> builder)
        {
            builder.HasKey(p => p.BankAccountId);

            builder.Property(p => p.AccountNumber)
                .IsRequired();
            builder.HasIndex(p => p.AccountNumber)
                .IsUnique();

            builder.Property(p => p.Balance)
                .HasDefaultValue(0.0m);

            builder.HasOne(c => c.BankAccCustomer)
                .WithOne(ba => ba.BankAccount)
                .HasForeignKey<CustomerBankAcc>(c => c.BankAccountId);
        }
    }
}
