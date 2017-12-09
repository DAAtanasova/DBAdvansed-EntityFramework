using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamBuilder.Models;

namespace TeamBuilder.Data.Configuration
{
    public class TeamEventConfig : IEntityTypeConfiguration<TeamEvent>
    {
        public void Configure(EntityTypeBuilder<TeamEvent> builder)
        {
            builder.HasKey(p => new { p.TeamId, p.EventId });

            builder.HasOne(te => te.Team)
                .WithMany(t => t.TeamEvents)
                .HasForeignKey(te => te.TeamId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(te => te.Event)
                .WithMany(e => e.EventTeams)
                .HasForeignKey(te => te.EventId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
