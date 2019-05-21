using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KitchenApp.Models
{
    public class Notification : Entity
    {
        public Notification(KitchenAppContext context) : base(context)
        {

        }

        protected Notification() : base()
        {

        }

        public virtual Menu SelectedMenu { get; set; }
        public virtual string Message { get; set; }
    }
}
