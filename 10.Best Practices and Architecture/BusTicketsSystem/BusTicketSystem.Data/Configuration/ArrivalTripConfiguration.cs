using System;
using System.Collections.Generic;
using System.Text;
using BusTicketSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusTicketSystem.Data.Configuration
{
    public class ArrivalTripConfiguration : IEntityTypeConfiguration<ArrivedTrip>
    {
        public void Configure(EntityTypeBuilder<ArrivedTrip> builder)
        {
            builder.HasKey(p => p.ArrivalTripId);

            builder.Property(p => p.ActualArrivalTime)
                .HasDefaultValueSql("GETDATE()");
        }
    }
}
