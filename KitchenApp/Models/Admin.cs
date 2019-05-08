using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KitchenApp.DateProvider;

namespace KitchenApp.Models
{
    public class Admin : User
    {
        public Admin(KitchenAppContext _context) : base(_context)
        {
        }

        Menu GetTodaysMenu()
        {
           return Context.Orders.FirstOrDefault(d => d.Date == DateTime.Today)?.Menu;
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
            Order order = new Order() {Id= new Guid(), Date = DateTime.Today};
            order.Menu = menu;                
            menu.Orders.Add(order);
        }
        public override string Role { get { return Helper.ADMIN_ROLE; } }

        public void SetPrice(decimal price)
        {
            if(Details.FirstOrDefault().Order != null)
            {
                Details.FirstOrDefault().Order.Price = price;
            }
        }

        public void AddPayment(User user, decimal amount)
        {
            Payment payment = new Payment() { User = user, Amount = amount, DateTime = DateTime.Now };
            PaymentDetail paymentDetail = new PaymentDetail() { Payment = payment, OrderDetail = this.Details.FirstOrDefault() };
            payment.Details.Add(paymentDetail);

            Payments.Add(payment);
        }

        public void CloseOrderOfToday()
        {
            var menu = GetTodaysMenu();
            if (menu!=null)
            {
                var order = menu.Orders.Single(a => a.Date == DateTime.Today);
                order.IsClosed = true;
            }
            else
            {
                throw new Exception("Menu was not selected for today");
            }
        }
    }
}
