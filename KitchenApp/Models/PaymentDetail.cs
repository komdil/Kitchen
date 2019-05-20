using KitchenApp.Models;
using System;

namespace KitchenApp.Models
{
    public class PaymentDetail : Entity
    {
        public PaymentDetail(KitchenAppContext context) : base(context)
        {

        }

        protected PaymentDetail() : base()
        {

        }
        public virtual Payment Payment { get; set; }
        public Guid PaymentId { get; set; }
        public DateTime DateTime { get; set; }
        public decimal PaidAmount { get; set; }
    }
}
