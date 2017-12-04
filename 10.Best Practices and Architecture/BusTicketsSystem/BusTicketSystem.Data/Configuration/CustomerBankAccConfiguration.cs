using System;
using System.Collections.Generic;
using System.Text;
using BusTicketSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusTicketSystem.Data.Configuration
{
    public class CustomerBankAccConfiguration : IEntityTypeConfiguration<CustomerBankAcc>
    {
        public void Configure(EntityTypeBuilder<CustomerBankAcc> builder)
        {
            builder.HasKey(p => new
            {
                p.CustomerId,
                p.BankAccountId
            });
        }
    }
}
