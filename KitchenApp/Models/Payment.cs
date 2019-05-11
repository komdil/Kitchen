using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KitchenApp.Models
{
    public class Payment
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
        //TODO There should be payment status bool type (IsPaid)
        public DateTime DateTime { get; set; }
        public decimal Amount { get; set; }
        public virtual List<PaymentDetail> Details { get; set; } = new List<PaymentDetail>();
        public virtual User User { get; set; }
        public Guid UserId { get; set; }
    }
}
