using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KitchenApp.Models
{
    public class Order
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
        public DateTime Date { get; set; }
        public Guid MenuId { get; set; }
        public virtual Menu Menu { get; set; }
        //TODO it should be virtual
        public bool Closed { get; set; }
        public decimal Price { get; set; }
        //TODO it should be virtual
        public int PeopleCount { get; set; }
        public virtual List<OrderDetail> Details { get; set; }

    }
}
