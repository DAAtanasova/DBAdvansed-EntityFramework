using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using P03_SalesDatabase.Data.Models;

namespace P03_SalesDatabase.Data
{
    public class SalesContext : DbContext
    {
        public SalesContext()
        {
        }

        public SalesContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Sale> Sales { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Store> Stores { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);
            if (!builder.IsConfigured)
            {
                builder.UseSqlServer(Configuration.connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Product>(entity =>
            {
                entity.HasKey(p => p.ProductId);

                entity.Property(p => p.Name)
                    .IsRequired()
                    .IsUnicode()
                    .HasMaxLength(50);
                entity.Property(p => p.Quantity)
                    .IsRequired()
                    .HasDefaultValue(0);
                entity.Property(p => p.Price)
                    .IsRequired()
                    .HasDefaultValue(0);
                entity.Property(p => p.Description)
                    .HasMaxLength(250)
                    .HasDefaultValue("No description");
            });

            builder.Entity<Customer>(entity =>
            {
                entity.HasKey(p => p.CustomerId);

                entity.Property(p => p.Name)
                    .IsRequired()
                    .IsUnicode()
                    .HasMaxLength(100);
                entity.Property(p => p.Email)
                    .IsUnicode(false)
                    .HasMaxLength(80);
            });

            builder.Entity<Store>(entity =>
            {
                entity.HasKey(p => p.StoreId);

                entity.Property(p => p.Name)
                    .IsRequired()
                    .IsUnicode()
                    .HasMaxLength(80);
            });

            builder.Entity<Sale>(entity =>
            {
                entity.HasKey(p => p.SaleId);

                entity.Property(p => p.Date)
                    .HasDefaultValueSql("GETDATE()");

                entity.HasOne(p => p.Customer)
                    .WithMany(p => p.Sales)
                    .HasForeignKey(p => p.CustomerId);
                entity.HasOne(p => p.Product)
                    .WithMany(p => p.Sales)
                    .HasForeignKey(p => p.SaleId);
                entity.HasOne(p => p.Store)
                    .WithMany(p => p.Sales)
                    .HasForeignKey(p => p.StoreId);
            });
        }
    }
}
