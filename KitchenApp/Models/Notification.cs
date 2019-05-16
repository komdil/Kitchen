using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KitchenApp.Models
{
    public class Notification:Entity
    {
        public virtual Menu selectedMenu { get; set; }
        public virtual string Message { get; set; }
    }
}
