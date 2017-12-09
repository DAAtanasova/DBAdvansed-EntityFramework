using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamBuilder.Models;

namespace TeamBuilder.Data.Configuration
{
    public class TeamConfig : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.HasKey(p => p.TeamId);
            builder.Property(p => p.TeamId)
                .UseSqlServerIdentityColumn();

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(25);
            builder.HasIndex(p => p.Name)
                .IsUnique();

            builder.Property(p => p.Description)
                .HasMaxLength(32);

            builder.Property(p => p.Acronym)
                .IsRequired()
                .HasColumnType("char(3)");

            builder.HasOne(c => c.Creator)
                .WithMany(t => t.CreatedTeams)
                .HasForeignKey(t => t.CreatorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
