using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KitchenApp.Models;
using KitchenApp.Models.Exceptions;
using KitchenApp.ViewsModel;

namespace KitchenApp.Models
{
    public class Admin : User
    {

        public void GetAdmin()
        {

        }
        public Admin(KitchenAppContext context) : base(context)
        {
            Context = context;
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

        public void CreateNewMenu(string name, string description)
        {
            var menu = Context.GetEntities<Menu>().Any(m => m.Name == name);
            if (menu)
            {
                throw new MenuAleadyIsExsistException(name);
            }
            else
            {
                Menu menuNew = new Menu(Context);
                menuNew.Name = name;
                menuNew.Description = description;
                Context.Add(menuNew);
                Context.SaveChanges();
            }

        }

        public void UpdateMenu(Guid id, string name, string description)
        {
            var menu = Context.GetEntities<Menu>().FirstOrDefault(m => m.Id == id);
            if (menu != null)
            {
                menu.Name = name;
                menu.Description = description;
                Context.SaveChanges();
            }
            else
            {
                throw new MenuWasNotFoundException(id);
            }
        }

        public void CreateNewUser(string firstName, string lastName, string login, string password)
        {
            var user = Context.GetEntities<User>().Any(u => u.Login == login);
            if (user)
            {
                throw new UserIsAlreadyExsist(login);
            }
            else
            {
                User userNew = new User(Context);
                userNew.FirstName = firstName;
                userNew.LastName = lastName;
                userNew.Login = login;
                userNew.Password = password;
                Context.Add(userNew);
                Context.SaveChanges();




            }

        }


    }





}

