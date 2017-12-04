using System;
using System.Collections.Generic;
using System.Text;
using BusTicketSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusTicketSystem.Data.Configuration
{
    public class BusCompanyConfiguration : IEntityTypeConfiguration<BusCompany>
    {
        public void Configure(EntityTypeBuilder<BusCompany> builder)
        {
            builder.HasKey(p => p.BusCompanyId);

            builder.Property(p => p.Name)
                .IsRequired();

            builder.Property(p => p.Nationality)
                .IsRequired(false);
            
            builder.HasMany(b => b.Reviews)
                .WithOne(r => r.BusCompany)
                .HasForeignKey(r => r.BusCompanyId);

            builder.HasMany(bc => bc.Trips)
                .WithOne(t => t.BusCompany)
                .HasForeignKey(t => t.BusCompanyId);
        }
    }
}
