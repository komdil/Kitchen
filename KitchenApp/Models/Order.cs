using KitchenApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace KitchenApp.Models
{
    public class Order : Entity
    {
        public Order(KitchenAppContext context) : base(context)
        {

        }

        protected Order() : base()
        {

        }
        public DateTime Date { get; set; }
        public Guid MenuId { get; set; }
        public virtual Menu Menu { get; set; }
        public bool IsClosed { get; set; }
        public decimal Price { get; set; }
        public virtual int PeopleCount { get { return Details.Count; } }

        public virtual decimal PriceForEach { get { return Price / PeopleCount; } }

        public virtual List<OrderDetail> Details { get; set; } = new List<OrderDetail>();
    }
}
