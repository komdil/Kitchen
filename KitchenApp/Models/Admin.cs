using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KitchenApp.DateProvider;
using KitchenApp.Models.Exceptions;

namespace KitchenApp.Models
{
    public class Admin : User
    {
        public Admin(KitchenAppContext context) : base(context)
        {

        }
        public Admin():base()
        {

        }
        public void AddNotificationToAllUsers(Notification notification)
        {

        }
        public Menu GetTodaysMenu()
        {
            var idMenu = Context.Orders.FirstOrDefault(d => d.Date == DateTime.Today)?.MenuId;
            return Context.Menus.FirstOrDefault(d => d.Id == idMenu);
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
            if (Context.Orders.Any(o => o.Date == DateTime.Today))
            {
                throw new MenuAlreadySelectedException();
            }
            else
            {
                Order order = new Order(Context) { Date = DateTime.Today };
                order.Menu = menu;
                Context.Orders.Add(order);
                Context.SaveChanges();
            }
        }
        public override string Role { get { return Helper.ADMIN_ROLE; } }

        public void SetPrice(decimal price)
        {
            if (Details.FirstOrDefault().Order != null)
            {
                Details.FirstOrDefault().Order.Price = price;
            }
        }

        public void AddPayment(User user, decimal amount)
        {
            Payment payment = new Payment(Context) { User = user, Amount = amount, DateTime = DateTime.Now };
            PaymentDetail paymentDetail = new PaymentDetail(Context) { Payment = payment, OrderDetail = this.Details.FirstOrDefault() };
            payment.Details.Add(paymentDetail);

            Payments.Add(payment);
        }

        public void CloseOrderOfToday()
        {
            var menu = GetTodaysMenu();
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
    }
}
