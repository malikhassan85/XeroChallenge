using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using XeroChallenge.Domain.Entities;

namespace XeroChallenge.Infrastructure.Persistence.DBContext
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().ToTable("Products");
            modelBuilder.Entity<ProductOption>().ToTable("ProductOptions");
            modelBuilder.Entity<Product>().HasKey(p => p.Id);
            modelBuilder.Entity<ProductOption>().HasKey(p => p.Id);
            modelBuilder.Entity<Product>()
           .HasMany(b => b.Options).WithOne(p=>p.Product).HasForeignKey("ProductId").OnDelete(DeleteBehavior.Cascade).IsRequired();
        }
    }
}
