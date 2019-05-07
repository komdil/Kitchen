using Microsoft.EntityFrameworkCore;
using KitchenApp.Models;

namespace KitchenApp.DateProvider
{
    public class KitchenAppContext : DbContext

    {
        public KitchenAppContext(DbContextOptions<KitchenAppContext> options)
            : base(options)
        {
            Database.EnsureDeleted();
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
            etBuilder.HasOne(o => o.Menu).WithMany(a => a.Orders).HasForeignKey(f => f.MenuId);
            etBuilder.HasMany(o => o.Details).WithOne(d => d.Order).HasForeignKey(f => f.OrderId);
        }

        void MenuMapping(ModelBuilder builder)
        {
            var etBuilder = builder.Entity<Menu>();
            etBuilder.HasKey(m => new { m.Id });
            etBuilder.HasMany(m => m.Orders).WithOne(d => d.Menu).HasForeignKey(f => f.MenuId);
        }

        void UserMapping(ModelBuilder builder)
        {
            var etBuilder = builder.Entity<User>();
            etBuilder.HasKey(u => new { u.Id });
            etBuilder.HasMany(u => u.Details).WithOne(d => d.User).HasForeignKey(f => f.UserId);
            etBuilder.HasMany(u => u.Payments).WithOne(d => d.User).HasForeignKey(f => f.UserId);

        }
        void PaymentMapping(ModelBuilder builder)
        {
            var etBuilder = builder.Entity<Payment>();
            etBuilder.HasKey(p => new { p.Id });
            etBuilder.HasOne(p => p.User).WithMany(m => m.Payments).HasForeignKey(f => f.UserId);
            etBuilder.HasMany(p => p.Details).WithOne(d => d.Payment);
        }
        void OrderDetailMapping(ModelBuilder builder)
        {
            var etBuilder = builder.Entity<OrderDetail>();
            etBuilder.HasKey(o => new { o.Id });
            etBuilder.HasOne(o => o.User);
            etBuilder.HasMany(o => o.Payments).WithOne(d => d.OrderDetail).HasForeignKey(f => f.OrderDetailId);
        }
        void PaymentDetailMapping(ModelBuilder builder)
        {
            var etBuilder = builder.Entity<PaymentDetail>();
            etBuilder.HasKey(p => new { p.Id });
            etBuilder.HasOne(p => p.OrderDetail).WithMany(m => m.Payments).HasForeignKey(f => f.OrderDetailId);
            etBuilder.HasOne(p => p.Payment).WithMany(m => m.Details);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PaymentDetail> PaymentDetails { get; set; }
    }
}

