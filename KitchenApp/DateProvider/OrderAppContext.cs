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
             etBuilder.HasKey(o => new { o.Id });
            
            etBuilder.HasOne(o => o.Menu);
            etBuilder.HasMany(o => o.Details).WithOne(d => d.Order);

        }
        void MenuMapping(ModelBuilder builder)
        {


            var etBuilder = builder.Entity<Menu>();
            etBuilder.HasKey(m => new { m.Id });
            etBuilder.HasMany(m => m.Orders).WithOne(d => d.Menu);
        }
        void UserMapping(ModelBuilder builder)
        {
            var etBuilder = builder.Entity<User>();
            etBuilder.HasKey(u => new { u.Id });
            etBuilder.HasMany(u => u.Details).WithOne(d => d.User);
            etBuilder.HasMany(u => u.Payments).WithOne(d => d.User);

        }
        void PaymentMapping(ModelBuilder builder)
        {
            var etBuilder = builder.Entity<Payment>();
            etBuilder.HasKey(p => new { p.Id });
            etBuilder.HasMany(p => p.Details).WithOne(d => d.Payment);
            etBuilder.HasOne(p => p.User);
        }
        void OrderDetailMapping(ModelBuilder builder)
        {
            var etBuilder = builder.Entity<OrderDetail>();
            etBuilder.HasKey(o => new { o.Id });
            etBuilder.HasOne(o => o.User);
            etBuilder.HasMany(o => o.Payments).WithOne(d => d.OrderDetail);

        }
        void PaymentDetailMapping(ModelBuilder builder)
        {
            var etBuilder = builder.Entity<PaymentDetail>();
            etBuilder.HasKey(p => new { p.Id });
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

