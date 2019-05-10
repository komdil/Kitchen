using System;
using System.Collections.Generic;

namespace KitchenApp.Models
{
    public class OrderDetail
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
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public virtual List<PaymentDetail> Payments { get; set; }
        public virtual Order Order { get; set; }
        public Guid OrderId { get; set; }
    }
}
