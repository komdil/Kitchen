using KitchenApp.DateProvider;
using System;

namespace KitchenApp.Models
{
    public class PaymentDetail : Entity
    {
        public PaymentDetail(KitchenAppContext context) : base(context)
        {

        }

        public PaymentDetail() : base()
        {

        }
        public virtual Payment Payment { get; set; }
        public Guid OrderDetailId { get; set; }
        public virtual OrderDetail OrderDetail { get; set; }
    }
}
