using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductsShop.Models;

namespace ProductsShop.Data.Configuration
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(p => p.UserId);

            builder.Property(p => p.FirstName)
                .IsRequired(false)
                .IsUnicode();

            builder.Property(p => p.LastName)
                .IsRequired()
                .IsUnicode();

            builder.Property(p => p.Age)
                .IsRequired(false);
            
        }
    }
}
