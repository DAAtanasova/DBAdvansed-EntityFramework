using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamBuilder.Models;

namespace TeamBuilder.Data.Configuration
{
    public class InvitationConfig : IEntityTypeConfiguration<Invitation>
    {
        public void Configure(EntityTypeBuilder<Invitation> builder)
        {
            builder.HasKey(p => p.InvitationId);

            // builder.Property(p => p.IsActive).HasDefaultValue(true);

            builder.HasOne(i => i.InvitedUser)
                .WithMany(u => u.Invitations)
                .HasForeignKey(i => i.InvitedUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(i => i.Team)
                .WithMany(t => t.Invitations)
                .HasForeignKey(i => i.TeamId);
                //.OnDelete(DeleteBehavior.Restrict);
        }
    }
}
