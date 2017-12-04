using System;
using System.Collections.Generic;
using System.Text;
using BusTicketSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusTicketSystem.Data.Configuration
{
    public class TripConfiguration : IEntityTypeConfiguration<Trip>
    {
        public void Configure(EntityTypeBuilder<Trip> builder)
        {
            builder.HasKey(p => p.TripId);

            builder.Property(p => p.DepartureTime)
                .IsRequired();

            builder.Property(p => p.ArrivalTime)
                .IsRequired(false);

            builder.Property(p => p.Status)
                .IsRequired();
            
            builder.HasOne(t => t.DestinationBusStation)
                .WithMany(dbs => dbs.TripsArrivedAt)
                .HasForeignKey(t => t.DestinationBusStationId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.OriginBusStation)
                .WithMany(obs => obs.TripsGoFrom)
                .HasForeignKey(t => t.OriginBusStationId)
                .OnDelete(DeleteBehavior.Restrict);
            
            builder.HasOne(t => t.BusCompany)
                .WithMany(bc => bc.Trips)
                .HasForeignKey(t => t.BusCompanyId);
        }
    }
}
