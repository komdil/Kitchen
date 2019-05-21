using KitchenApp.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using KitchenApp.Models.Exceptions;

namespace KitchenApp.Models
{
    public class User : Entity
    {
        public User(KitchenAppContext context) : base(context)
        {

        }
        protected User() : base()
        {

        }
        public void AcceptMenu(Menu menu)
        {
            if (menu.Orders.Count != 0)//order created
            {
                OrderDetail orderDetail = new OrderDetail(Context) { User = this, Order = menu.Orders.First() };
                Context.SaveChanges();
            }
            else throw new MenuWasNotSelectedForTodayException();
        }

        public void RejectMenu(Menu menu)
        {
            var order = menu.Orders.First();
            if (order.Price == 0)
            {
                OrderDetail orderDetail = Context.GetEntities<OrderDetail>().FirstOrDefault(a => a.User == this && a.Order == order);
                if (orderDetail != null)
                {
                    Context.Remove(orderDetail);
                    Context.SaveChanges();
                }
            }
            else
            {
                throw new PriceAlreadySetException();
            }
        }

        public string Login { get; set; }
        [Required(ErrorMessage = "Password is not assignet")]
        public string Password { get; set; }
        public string Salt { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual List<Payment> Payments { get; set; }
        public virtual List<OrderDetail> Details { get; set; } = new List<OrderDetail>();
        public virtual string Role { get { return Helper.USER_ROLE; } }
        public virtual decimal Balance
        {
            get
            {
                return 0;
            }
        }
        public virtual List<Notification> Notifications { get; set; } = new List<Notification>();

    }
}