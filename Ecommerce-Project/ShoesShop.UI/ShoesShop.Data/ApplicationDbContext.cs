using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShoesShop.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoesShop.Domain;

namespace ShoesShop.Data
{
    public class ApplicationDbContext : DbContext
    {
        public virtual DbSet<Activity> Activities { get; set; } = null!;
        public virtual DbSet<Admin> Admins { get; set; } = null!;
        public DbSet<ShoesShop.Domain.Attribute> Attributes { get; set; } = null!;
        public virtual DbSet<AttributeValue> AttributeValues { get; set; } = null!;
        public virtual DbSet<Catalog> Catalogs { get; set; } = null!;
        public virtual DbSet<CommentProduct> CommentProducts { get; set; } = null!;
        public virtual DbSet<Contact> Contacts { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<CustomerAddress> CustomerAddresses { get; set; } = null!;
        public virtual DbSet<ForgotPassword> ForgotPasswords { get; set; } = null!;
        public virtual DbSet<Manufacture> Manufactures { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderDetail> OrderDetails { get; set; } = null!;
        public virtual DbSet<Payment> Payments { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<ProductGallery> ProductGallerys { get; set; } = null!;
        public virtual DbSet<ProductAttribute> ProductAttributes { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=LAPTOP-76M9FLCU\\NTDUNG;Database=ShoesShopAssignment;Trusted_Connection=True;");
                //.LogTo(Console.WriteLine, new[]
                //{
                //    DbLoggerCategory.Model.Name,
                //    DbLoggerCategory.Database.Command.Name,
                //    DbLoggerCategory.Database.Transaction.Name,
                //    DbLoggerCategory.Query.Name,
                //    DbLoggerCategory.ChangeTracking.Name,
                //},
                //    LogLevel.Information)
                //.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Config many to many relationship entity
            modelBuilder.Entity<ProductAttribute>().HasKey(r => new { r.ProductId, r.AttributeValueId });
            modelBuilder.Entity<CommentProduct>().HasKey(r => new { r.ProductId, r.CustomerId });
            modelBuilder.Entity<OrderDetail>().HasKey(r => new { r.OrderId, r.ProductId, r.AttributeValueId });

            // Set unique for property
            modelBuilder.Entity<Customer>().HasIndex(u => u.Email).IsUnique();
            modelBuilder.Entity<Admin>().HasIndex(u => u.UserName).IsUnique();
        }
    }
}
