using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KitchenApp.Models
{
    public class PaymentDetail
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

        public Guid PaymentId { get; set; }
        public virtual Payment Payment { get; set; }

        public Guid OrderDetailId { get; set; }
        public virtual OrderDetail OrderDetail { get; set; }
    }
}
