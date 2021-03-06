﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KitchenApp.Models;
using KitchenApp.Models.Exceptions;

namespace KitchenApp.Models
{
    public class Admin : User
    {
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
            var order = menu.Orders.Single(a => a.Date == DateTime.Today);
            order.IsClosed = (!order.IsClosed) ? true : throw new OrderAlreadyClosedException();
            Context.SaveChanges();
        }
    }
}
