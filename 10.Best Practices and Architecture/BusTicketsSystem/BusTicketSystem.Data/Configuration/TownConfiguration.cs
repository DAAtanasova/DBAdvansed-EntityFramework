using System;
using System.Collections.Generic;
using System.Text;
using BusTicketSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusTicketSystem.Data.Configuration
{
    public class TownConfiguration : IEntityTypeConfiguration<Town>
    {
        public void Configure(EntityTypeBuilder<Town> builder)
        {
            builder.HasKey(p => p.TownId);

            builder.Property(p => p.Name)
                .IsRequired();

            builder.Property(p => p.Country)
                .IsRequired();
        }
    }
}
