using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public string Login { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public virtual List<Payment> Payments { get; set; }
        public virtual List<OrderDetail> Details { get; set; }
    }
}
