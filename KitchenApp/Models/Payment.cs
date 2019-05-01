using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderApp.Models
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
        public DateTime DateTime { get; set; }
        public int Amount { get; set; }
        public virtual List<PaymentDetail> Details { get; set; }
        public virtual User User { get; set; }
    }
}
