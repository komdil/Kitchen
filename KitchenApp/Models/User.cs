using KitchenApp.DateProvider;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KitchenApp.Models
{
    public class User : Entity
    {
        Guid id;

        public User(KitchenAppContext _context):base(_context)
        {
        }

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

        public void AcceptMenu(Menu menu)
        {
            if (menu.Orders.Count != 0)//order created
            {
                OrderDetail orderDetail = new OrderDetail() { Id = new Guid(), User = this, Order = menu.Orders.First() };
                menu.Orders.First().Details.Add(orderDetail);
            }
            else throw new Exception("Order is not created");
        }

        public string Login { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual bool IsAdmin { get { return this is Admin; } }
        public virtual List<Payment> Payments { get; set; }
        public virtual List<OrderDetail> Details { get; set; } = new List<OrderDetail>();

        public virtual string Role { get { return Helper.USER_ROLE; } }
    }
}
