using Microsoft.EntityFrameworkCore;
using NET_Core_API.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NET_Core_API.Data.Repositories
{
    public class ProductContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<ProductCategory> ProductCategories { get; set; }


        public ProductContext(DbContextOptions<ProductContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProductCategory>().HasKey(p => new { p.CategoryID, p.ProductID });

            modelBuilder.Entity<ProductCategory>()
                .HasOne(p => p.Product)
                .WithMany(c => c.Categories)
                .HasForeignKey(p => p.ProductID);

            modelBuilder.Entity<ProductCategory>()
                 .HasOne(p => p.Category)
                 .WithMany(c => c.Products)
                 .HasForeignKey(p => p.CategoryID);
        }
    }
}
