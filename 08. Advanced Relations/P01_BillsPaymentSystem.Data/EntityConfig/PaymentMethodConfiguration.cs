using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P01_BillsPaymentSystem.Data.Models;

namespace P01_BillsPaymentSystem.Data.EntityConfig
{
    public class PaymentMethodConfiguration : IEntityTypeConfiguration<PaymentMethod>
    {
        public void Configure(EntityTypeBuilder<PaymentMethod> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(u => u.UserId)
                .IsRequired();

            builder.Property(t => t.Type)
                .IsRequired();

            builder.HasOne(pm => pm.User)
                .WithMany(u => u.PaymentMethods)
                .HasForeignKey(pm => pm.UserId);

            builder.HasOne(pm => pm.CreditCard)
                .WithOne(c => c.PaymentMethod)
                .HasForeignKey<PaymentMethod>(a => a.CreditCardId);

            builder.HasOne(pm => pm.BankAccount)
                .WithOne(ba =>ba.PaymentMethod)
                .HasForeignKey<PaymentMethod>(pm => pm.BankAccountId);
            
            builder.HasIndex(pm => new { pm.UserId, pm.CreditCardId, pm.BankAccountId })
                .IsUnique();


        }
    }
}
