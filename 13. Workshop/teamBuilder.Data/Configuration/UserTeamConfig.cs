using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamBuilder.Models;

namespace TeamBuilder.Data.Configuration
{
    public class UserTeamConfig : IEntityTypeConfiguration<UserTeam>
    {
        public void Configure(EntityTypeBuilder<UserTeam> builder)
        {
            builder.HasKey(p => new { p.UserId, p.TeamId });

            builder.HasOne(ut => ut.User)
                .WithMany(u => u.UserTeams)
                .HasForeignKey(ut => ut.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ut => ut.Team)
                .WithMany(t => t.UsersTeam)
                .HasForeignKey(ut => ut.TeamId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
