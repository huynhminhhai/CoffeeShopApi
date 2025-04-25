using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeShopApi.Model;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShopApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the Order.Status property
            modelBuilder.Entity<Order>()
                .Property(o => o.Status)
                .HasConversion<string>();
        }

    }
}