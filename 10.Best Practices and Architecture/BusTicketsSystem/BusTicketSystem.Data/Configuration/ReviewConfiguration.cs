using System;
using System.Collections.Generic;
using System.Text;
using BusTicketSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BusTicketSystem.Models.Validation;

namespace BusTicketSystem.Data.Configuration
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasKey(p => p.ReviewId);

            builder.Property(p => p.Content)
                .IsRequired(false);

            builder.Property(p => p.Grade)
                .IsRequired();

            builder.Property(p => p.PublishedOn)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

            builder.HasOne(r => r.BusCompany)
                .WithMany(bc => bc.Reviews)
                .HasForeignKey(r=>r.BusCompanyId);

            builder.HasOne(r => r.Customer)
                .WithMany(c => c.Reviews)
                .HasForeignKey(r => r.CustomerId);
        }
    }
}
