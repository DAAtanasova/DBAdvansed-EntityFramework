using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamBuilder.Models;

namespace TeamBuilder.Data.Configuration
{
    public class EventConfig : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.HasKey(p => p.EventId);
            builder.Property(p => p.EventId)
                .UseSqlServerIdentityColumn();

            builder.Property(p => p.Name)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(25);

            builder.Property(p => p.Description)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(250);

            builder.HasOne(e => e.Creator)
                .WithMany(c => c.CreatedEvents)
                .HasForeignKey(e => e.CreatorId)
                .OnDelete(DeleteBehavior.Restrict);
            
        }
    }
}
