using KitchenApp.DateProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

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
        public void AcceptMenu(Menu menu)
        {
            if (menu.Orders.Count != 0)//order created
            {
                OrderDetail orderDetail = new OrderDetail(Context) { Id = new Guid(), User = this, Order = menu.Orders.First() };
                menu.Orders.First().Details.Add(orderDetail);
            }
            else throw new Exception("Order is not created");
        }

        public string Login { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual bool IsAdmin { get { return this is Admin; } }
        public virtual List<Payment> Payments { get; set; }

        public virtual List<OrderDetail> Details { get; set; } = new List<OrderDetail>();
        public virtual string Role { get { return Helper.USER_ROLE; } }

        //public virtual decimal Balance { get; set; }
        public virtual List<Notification> Notifications { get; set; } = new List<Notification>();

    }
}