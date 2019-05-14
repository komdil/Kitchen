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
        public Admin() : base()
        {

        }
        public Menu GetTodaysMenu()
        {
            var idMenu = Context.Orders.FirstOrDefault(d => d.Date == DateTime.Today)?.MenuId;
            return Context.Menus.FirstOrDefault(d => d.Id == idMenu);
        }
        public List<Order> GetListOfOrders() => Context.Orders.ToList();

        public List<Payment> GetPayments() => Context.Payments.ToList();

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

        public void SetPrice(Order order, decimal price)
        {
            var currentOrder = Context.Orders.SingleOrDefault(o => o.Id == order.Id);
            if (currentOrder == null)
                throw new OrderDoesNotExistException();
            else if (currentOrder.Price != 0)
            {
                throw new PriceAlreadySetException();
            }
            else
            {
                currentOrder.Price = price;
                Context.SaveChanges();
            }
        }

        public void AddPayment(OrderDetail orderDetail, decimal amount)
        {
            Order currentOrder = Context.Orders.SingleOrDefault(o => o.Id == orderDetail.Order.Id);
            if (currentOrder == null)
                throw new OrderDoesNotExistException();
            else
            {
                Payment payment = new Payment(Context) { User = orderDetail.User, Amount = amount, DateTime = DateTime.Now };
                PaymentDetail paymentDetail = new PaymentDetail(Context) { Payment = payment, OrderDetail = orderDetail };
                payment.Details.Add(paymentDetail);

                orderDetail.User.Payments.Add(payment);
                Context.SaveChanges();
            }
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
