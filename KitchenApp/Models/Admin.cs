using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KitchenApp.Models;
using KitchenApp.Models.Exceptions;

namespace KitchenApp.Models
{
    public class Admin : User
    {

        public void GetAdmin()
        {

        }
        public Admin(KitchenAppContext context) : base(context)
        {

        }
        protected Admin() : base()
        {

        }
        public void AddNotificationToAllUsers(Notification notification)
        {

        }
        public void AddNewUser(User user)
        {
            throw new NotImplementedException();
        }

        public void Delete(User user)
        {
            throw new NotImplementedException();
        }
        public void SelectMenuForToday(Menu menu)
        {
            try
            {
                Context.GetSelectedMenuForToday();
            }
            catch (MenuWasNotSelectedForTodayException)
            {
                Order order = new Order(Context) { Date = DateTime.Today, Menu = menu };
                Context.SaveChanges();
                return;
            }
            throw new MenuAlreadySelectedException();
        }
        public override string Role { get { return Helper.ADMIN_ROLE; } }

        public void SetPrice(decimal price)
        {
            var todaysMenu = Context.GetSelectedMenuForToday();
            todaysMenu.Orders.First().Price = price;
            Context.SaveChanges();
        }

        public void AddPayment(User user, decimal amount)
        {

        }

        public void CloseOrderOfToday()
        {
            var menu = Context.GetSelectedMenuForToday();
            if (menu != null)
            {
                var order = menu.Orders.Single(a => a.Date == DateTime.Today);
                order.IsClosed = (!order.IsClosed) ? true : throw new OrderAlreadyClosedException();
                Context.SaveChanges();
            }
            else
            {
                throw new MenuWasNotSelectedForTodayException();
            }
        }

        public int CreateNewMenu(string name, string description)
        {
            if (menuIsAlreadyExsist(name))
            {
                return Context.SaveChanges();
            }
            else
            {
                Menu menu = new Menu(Context) { Name = name, Description = description };
                return Context.SaveChanges();
            }
        }

        public int UpdateMenu(Guid id, string name, string description)
        {
            var menu = Context.GetEntities<Menu>().FirstOrDefault(m => m.Id == id);
            if (menu != null)
            {
                menu.Name = name;
                menu.Description = description;
                return Context.SaveChanges();
            }
            else
            {
                throw new MenuWasNotFoundException(id);
            }
        }

        public bool menuIsAlreadyExsist(string name)
        {
            var menu = Context.GetEntities<Menu>().Any(m => m.Name == name);

            return menu;
        }
    }
}
