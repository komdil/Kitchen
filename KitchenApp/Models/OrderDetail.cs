using KitchenApp.Models;
using System;
using System.Collections.Generic;

namespace KitchenApp.Models
{
    public class OrderDetail : Entity
    {
        public OrderDetail(KitchenAppContext context) : base(context)
        {

        }

        public OrderDetail() : base()
        {

        }
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public virtual List<PaymentDetail> Payments { get; set; } = new List<PaymentDetail>();
        public virtual Order Order { get; set; }
        public Guid OrderId { get; set; }
    }
}
