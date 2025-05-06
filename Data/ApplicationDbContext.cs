using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeShopApi.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShopApi.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ORDERITEM 
            modelBuilder.Entity<OrderItem>(x => x.HasKey(o => new { o.OrderId, o.ProductId }));

            modelBuilder.Entity<OrderItem>()
                .HasOne(o => o.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(o => o.OrderId);
            
            modelBuilder.Entity<OrderItem>()
                .HasOne(o => o.Product)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(o => o.ProductId);

            // PRODUCTIMAGE
            modelBuilder.Entity<ProductImage>(x => x.HasKey(p => new { p.ImageId, p.ProductId }));

            modelBuilder.Entity<ProductImage>()
                .HasOne(p => p.Product)
                .WithMany(p => p.ProductImages)
                .HasForeignKey(p => p.ProductId);

            modelBuilder.Entity<ProductImage>()
                .HasOne(p => p.Image)
                .WithMany(p => p.ProductImages)
                .HasForeignKey(p => p.ImageId);

            // ROLE
            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = "1",
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Id = "2",
                    Name = "User",
                    NormalizedName = "USER"
                },
            };
            modelBuilder.Entity<IdentityRole>().HasData(roles);
        }

    }
}