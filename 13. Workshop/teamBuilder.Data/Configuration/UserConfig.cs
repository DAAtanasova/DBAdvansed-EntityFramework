using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamBuilder.Models;

namespace TeamBuilder.Data.Configuration
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(p => p.UserId);

            builder.Property(p => p.Username)
                .IsRequired()
                .HasMaxLength(25);
            builder.HasIndex(p => p.Username)
                .IsUnique();

            builder.Property(p => p.FirstName)
                .HasMaxLength(25);

            builder.Property(p => p.LastName)
                .HasMaxLength(25);

            builder.Property(p => p.Password)
                .IsRequired()
                .HasMaxLength(30);

           // builder.Property(p => p.IsDeleted) .HasDefaultValue(false);
        }
    }
}
