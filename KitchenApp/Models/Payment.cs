using System;
using System.Collections.Generic;
using System.Linq;

namespace KitchenApp.Models
{
    public class Payment : Entity
    {
        public Payment(KitchenAppContext context) : base(context)
        {

        }
        protected Payment() : base()
        {

        }
        public decimal SummAmount { get; set; }
        public virtual List<PaymentDetail> Details { get; set; } = new List<PaymentDetail>();
        public virtual User User { get; set; }
        public Guid UserId { get; set; }
        public virtual PaymentStatus Status
        {
            get
            {
                var summOfPayments = Details.Select(a => a.PaidAmount).Sum();
                if (summOfPayments >= SummAmount)
                {
                    return PaymentStatus.Paid;
                }
                else if (summOfPayments < SummAmount)
                {
                    return PaymentStatus.IsNotPaidAll;
                }
                else
                {
                    return PaymentStatus.IsNotPaid;
                }
            }
        }
    }
    public enum PaymentStatus
    {
        IsNotPaid,
        IsNotPaidAll,
        Paid,
        PaidMore
    }
}
