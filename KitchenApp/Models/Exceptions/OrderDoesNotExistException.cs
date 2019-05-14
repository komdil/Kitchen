using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KitchenApp.Models.Exceptions
{
    public class OrderDoesNotExistException:Exception
    {
        public OrderDoesNotExistException():base("Order doesn't exist!")
        {

        }
    }
}
