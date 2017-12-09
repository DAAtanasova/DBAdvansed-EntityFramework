using System;
using System.Collections.Generic;
using System.Text;
using Instagraph.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Instagraph.Data.Config
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Username)
                .IsRequired()
                .HasMaxLength(30);
            builder.HasAlternateKey(p => p.Username);

            builder.Property(p => p.Password)
                .IsRequired()
                .HasMaxLength(20);

            builder.HasOne(p => p.ProfilePicture)
                .WithMany(p => p.Users)
                .HasForeignKey(u => u.ProfilePictureId);
        }
    }
}
