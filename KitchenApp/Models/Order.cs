using System;
using System.Collections.Generic;

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
        public virtual bool IsClosed { get; set; }
        public decimal Price { get; set; }
        public virtual int PeopleCount { get { return Details.Count; } }
        public virtual List<OrderDetail> Details { get; set; }
    }
}
