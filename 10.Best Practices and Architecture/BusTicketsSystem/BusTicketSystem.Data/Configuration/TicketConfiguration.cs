using System;
using System.Collections.Generic;
using System.Text;
using BusTicketSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusTicketSystem.Data.Configuration
{
    public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
           builder.HasKey(t => t.TicketId);

            //builder.Property(t => t.TicketId)
            //    .ValueGeneratedOnAdd();

            builder.Property(p => p.Price)
                .IsRequired();

            builder.Property(p => p.Seat)
                .IsRequired();
            
            builder.HasOne(t => t.Customer)
                .WithMany(c => c.Tickets)
                .HasForeignKey(t => t.CustomerId);

            builder.HasOne(t => t.Trip)
                .WithMany(t => t.Tickets)
                .HasForeignKey(t => t.TripId);

            //if you try to put two customers no one seat for a certain trip
            builder.HasIndex(p => new
            {
                p.Seat,
                p.TripId
            }).IsUnique();
            
        }
    }
}
