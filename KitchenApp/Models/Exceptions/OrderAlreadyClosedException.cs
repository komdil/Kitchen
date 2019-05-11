using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KitchenApp.DateProvider;

namespace KitchenApp.Models.Exceptions
{
    public class OrderAlreadyClosedException : Exception
    {
        public OrderAlreadyClosedException() : base("You have already closed the order")
        {

        }
    }
}
