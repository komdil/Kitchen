using KitchenApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KitchenApp.Models
{
    public class Payment : Entity
    {
        public Payment(KitchenAppContext context) : base(context)
        {

        }

        public Payment() : base()
        {

        }
        public DateTime DateTime { get; set; }
        public decimal Amount { get; set; }
        public virtual List<PaymentDetail> Details { get; set; } = new List<PaymentDetail>();
        public virtual User User { get; set; }
        public Guid UserId { get; set; }
    }
}
