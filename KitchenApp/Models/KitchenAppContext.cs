using KitchenApp.Models.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace KitchenApp.Models
{
    public class KitchenAppContext : DbContext
    {
        public KitchenAppContext(DbContextOptions<KitchenAppContext> options)
            : base(options)
        {
            Database.EnsureCreated();
            BlankData.CreateBlankData(this);
        }

        #region Mapping
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OrderMapping(modelBuilder);
            MenuMapping(modelBuilder);
            UserMapping(modelBuilder);
            PaymentMapping(modelBuilder);
            OrderDetailMapping(modelBuilder);
            PaymentDetailMapping(modelBuilder);
            AdminMapping(modelBuilder);
        }

        void OrderMapping(ModelBuilder builder)
        {
            var etBuilder = builder.Entity<Order>();
            etBuilder.HasKey(m => new { m.Id });
            etBuilder.HasOne(o => o.Menu).WithMany(a => a.Orders).HasForeignKey(f => f.MenuId);
            etBuilder.HasMany(o => o.Details).WithOne(d => d.Order).HasForeignKey(f => f.OrderId);
        }

        void MenuMapping(ModelBuilder builder)
        {
            var etBuilder = builder.Entity<Menu>();
            etBuilder.HasKey(m => new { m.Id });
            etBuilder.HasMany(m => m.Orders).WithOne(d => d.Menu).HasForeignKey(f => f.MenuId);
        }

        void AdminMapping(ModelBuilder builder)
        {
            var etBuilder = builder.Entity<Admin>();
        }

        void UserMapping(ModelBuilder builder)
        {
            var etBuilder = builder.Entity<User>();
            etBuilder.HasKey(m => new { m.Id });
            etBuilder.HasMany(u => u.Details).WithOne(d => d.User).HasForeignKey(f => f.UserId);
            etBuilder.HasMany(u => u.Payments).WithOne(d => d.User).HasForeignKey(f => f.UserId);

        }
        void PaymentMapping(ModelBuilder builder)
        {
            var etBuilder = builder.Entity<Payment>();
            etBuilder.HasKey(m => new { m.Id });
            etBuilder.HasOne(p => p.User).WithMany(m => m.Payments).HasForeignKey(f => f.UserId);
            etBuilder.HasMany(p => p.Details).WithOne(d => d.Payment);
        }
        void OrderDetailMapping(ModelBuilder builder)
        {
            var etBuilder = builder.Entity<OrderDetail>();
            etBuilder.HasKey(m => new { m.Id });
            etBuilder.HasOne(o => o.User).WithMany(u => u.Details).HasForeignKey(f => f.UserId);
            etBuilder.HasOne(o => o.Order).WithMany(u => u.Details).HasForeignKey(f => f.OrderId);
        }
        void PaymentDetailMapping(ModelBuilder builder)
        {
            var etBuilder = builder.Entity<PaymentDetail>();
            etBuilder.HasKey(m => new { m.Id });
            etBuilder.HasOne(p => p.Payment).WithMany(m => m.Details);
        }

        #endregion

        public IQueryable<T> GetEntities<T>() where T : Entity
        {
            IQueryable<T> query = Set<T>();
            return new IContextable<T>(query, this);
        }

        public Menu GetSelectedMenuForToday()
        {
            var idMenu = GetEntities<Order>().FirstOrDefault(d => d.Date == DateTime.Today)?.MenuId;
            var menu = GetEntities<Menu>().FirstOrDefault(d => d.Id == idMenu);
            if (menu != null)
            {
                return menu;
            }
            else
            {
                throw new MenuWasNotSelectedForTodayException();
            }
        }

        public User GetUser(string login)
        {
            var user = GetEntities<User>().FirstOrDefault(u => u.Login == login);
            if (user != null)
            {
                user.Context = this;
                return user;
            }
            else
            {
                throw new UserWasNotFoundException(login);
            }
        }
    }
}

