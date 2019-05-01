using Microsoft.EntityFrameworkCore;
using KitchenApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KitchenApp.DateProvider
{
    public class KitchenAppContext : DbContext

    {
        public KitchenAppContext(DbContextOptions<KitchenAppContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OrderMapping(modelBuilder);
            MenuMapping(modelBuilder);
            UserMapping(modelBuilder);
            PaymentMapping(modelBuilder);
            OrderDetailMapping(modelBuilder);
            PaymentDetailMapping(modelBuilder);
        }

        void OrderMapping(ModelBuilder builder)
        {

            var etBuilder = builder.Entity<Order>();
            etBuilder.ToTable("Order");
            etBuilder.HasKey(o => new { o.Id });
            etBuilder.Property(o => o.Id).HasColumnName("Id");
            etBuilder.Property(o => o.Date).HasColumnName("Date");
            etBuilder.Property(o => o.Closed).HasColumnName("Closed");
            etBuilder.Property(o => o.Price).HasColumnName("Price");
            etBuilder.Property(o => o.PeopleCount).HasColumnName("PeopleCount");
            etBuilder.HasOne(o => o.Menu);
            etBuilder.HasMany(o => o.Details).WithOne(d => d.Order);

        }
        void MenuMapping(ModelBuilder builder)
        {
            var etBuilder = builder.Entity<Menu>();
            etBuilder.ToTable("Menu");
            etBuilder.HasKey(m => new { m.Id });
            etBuilder.Property(m => m.Id).HasColumnName("Id");
            etBuilder.Property(m => m.Name).HasColumnName("Name");
            etBuilder.Property(m => m.Description).HasColumnName("Description");
            etBuilder.HasMany(m => m.Orders).WithOne(d => d.Menu);
        }
        void UserMapping(ModelBuilder builder)
        {
            var etBuilder = builder.Entity<User>();
            etBuilder.ToTable("User");
            etBuilder.HasKey(u => new { u.Id });
            etBuilder.Property(u => u.Id).HasColumnName("Id");
            etBuilder.Property(u => u.Login).HasColumnName("Login");
            etBuilder.Property(u => u.Password).HasColumnName("Password");
            etBuilder.Property(u => u.IsAdmin).HasColumnName("IsAdmin");
            etBuilder.HasMany(u => u.Details).WithOne(d => d.User);
            etBuilder.HasMany(u => u.Payments).WithOne(d => d.User);

        }
        void PaymentMapping(ModelBuilder builder)
        {
            var etBuilder = builder.Entity<Payment>();
            etBuilder.ToTable("Payment");
            etBuilder.HasKey(p => new { p.Id });
            etBuilder.Property(p => p.Id).HasColumnName("Id");
            etBuilder.Property(p => p.DateTime).HasColumnName("DateTime");
            // etBuilder.Property(p => p.User).HasColumnName("User");
            etBuilder.Property(p => p.Amount).HasColumnName("Amount");
            // etBuilder.Property(p => p.Details).HasColumnName("Details");
            etBuilder.HasMany(p => p.Details).WithOne(d => d.Payment);
            etBuilder.HasOne(p => p.User);
        }
        void OrderDetailMapping(ModelBuilder builder)
        {
            var etBuilder = builder.Entity<OrderDetail>();
            etBuilder.ToTable("OrderDetail");
            etBuilder.HasKey(o => new { o.Id });
            etBuilder.Property(o => o.Id).HasColumnName("Id");
            etBuilder.Property(o => o.Amount).HasColumnName("Amount");
            //etBuilder.Property(o => o.Order).HasColumnName("Order");
            etBuilder.Property(o => o.OrderedDateTime).HasColumnName("OrderedDateTime");
           // etBuilder.Property(o => o.Payments).HasColumnName("Payments");
            etBuilder.Property(o => o.Reminder).HasColumnName("Reminder");
           // etBuilder.Property(o => o.User).HasColumnName("User");
            etBuilder.HasOne(o => o.User);
            etBuilder.HasMany(o => o.Payments).WithOne(d => d.OrderDetail);

        }
        void PaymentDetailMapping(ModelBuilder builder)
        {
            var etBuilder = builder.Entity<PaymentDetail>();
            etBuilder.ToTable("PaymentDetail");
            etBuilder.HasKey(p => new { p.Id });
            etBuilder.Property(p => p.Id).HasColumnName("Id");
           // etBuilder.Property(p => p.Payment).HasColumnName("Payment");
           // etBuilder.Property(p => p.OrderDetail).HasColumnName("OrderDetail");
            etBuilder.HasOne(p => p.OrderDetail);
            etBuilder.HasOne(p => p.Payment);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PaymentDetail> PaymentDetails { get; set; }


    }
}

