using System;
using System.Collections.Generic;

namespace KitchenApp.Models
{
    public class User
    {
        Guid id;
        public Guid Id
        {
            get
            {
                if (id == null || id == Guid.Empty)
                {
                    id = new Guid();
                }
                return id;
            }
            set
            {
                id = value;
            }
        }

        public void AcceptMenu(Menu menu, bool isAccepted)
        {
            if (isAccepted)
            {
                if (menu.Orders.Count != 0)//order created
                {
                    OrderDetail orderDetail = new OrderDetail() { Id = new Guid(), User = this, Order = menu.Orders.First() };
                    menu.Orders.First().Details.Add(orderDetail);
                }
                else throw new Exception("Order is not created");
            }
        }

        public string Login { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual bool IsAdmin { get { return this is model; } }
        public virtual List<Payment> Payments { get; set; }
        public virtual List<OrderDetail> Details { get; set; }

        public virtual string Role { get { return Helper.USER_ROLE; } }
    }
}
