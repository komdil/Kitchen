using KitchenApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace KitchenApp.Models
{
    public class OrderDetail : Entity
    {
        public OrderDetail(KitchenAppContext context) : base(context)
        {

        }

        protected OrderDetail() : base()
        {

        }
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public virtual Order Order { get; set; }
        public Guid OrderId { get; set; }
    }
}
