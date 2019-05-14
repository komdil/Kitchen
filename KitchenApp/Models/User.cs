using KitchenApp.DateProvider;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KitchenApp.Models
{
    public class User : Entity
    {
        public User(KitchenAppContext context) : base(context)
        {

        }
        public User() : base()
        {

        }
        public void AcceptMenu(Order order)
        {            
            OrderDetail orderDetail = new OrderDetail(Context) {User = this, Order = order };
            order.Details.Add(orderDetail);
            Context.SaveChanges();
        }
        public decimal Balance { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual bool IsAdmin { get { return this is Admin; } }
        public virtual List<Payment> Payments { get; set; } = new List<Payment>();
        public virtual List<OrderDetail> Details { get; set; } = new List<OrderDetail>();

        public virtual string Role { get { return Helper.USER_ROLE; } }
    }
}
