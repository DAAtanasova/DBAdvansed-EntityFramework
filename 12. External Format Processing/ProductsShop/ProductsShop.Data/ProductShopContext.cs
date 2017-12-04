using System;
using Microsoft.EntityFrameworkCore;
using ProductsShop.Data.Configuration;
using ProductsShop.Models;

namespace ProductsShop.Data 
{
    public class ProductShopContext : DbContext
    {
        public ProductShopContext()  { }

        public ProductShopContext(DbContextOptions options)
            :base(options) { }

        public DbSet<User> Users { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<CategoryProduct> CategoriesProducts { get; set; }

        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);
            if (!builder.IsConfigured)
            {
                builder.UseSqlServer(ServerConfig.connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserConfig());

            builder.ApplyConfiguration(new ProductConfig());

            builder.ApplyConfiguration(new CategoryConfig());

            builder.ApplyConfiguration(new CategoryProductConfig());
        }
    }
}
