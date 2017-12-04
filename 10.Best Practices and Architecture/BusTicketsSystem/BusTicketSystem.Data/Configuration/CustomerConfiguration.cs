using System;
using System.Collections.Generic;
using System.Text;
using BusTicketSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusTicketSystem.Data.Configuration
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(p => p.CustomerId);

            //builder.Property(p => p.CustomerId)
            //    .ValueGeneratedOnAdd();

            builder.Property(p => p.FirstName)
                .IsRequired();

            builder.Property(p => p.LastName)
                .IsRequired();

            builder.Property(p => p.Birthday)
                .IsRequired(false);

            builder.Property(p => p.Gender)
                .IsRequired()
                .HasDefaultValue(Gender.notSpecified);

            builder.HasOne(c => c.HomeTown)
                .WithMany(t => t.Customers)
                .HasForeignKey(c => c.HomeTownId);

            //builder.HasOne(c => c.BankAccount)
            //    .WithOne(ba => ba.Customer)
            //    .HasForeignKey<BankAccount>(b=>b.CustomerId);

            builder.HasOne(c => c.CustomerBankAccount)
                .WithOne(ba => ba.Customer)
                .HasForeignKey<CustomerBankAcc>(c => c.CustomerId);
        }
    }
}
