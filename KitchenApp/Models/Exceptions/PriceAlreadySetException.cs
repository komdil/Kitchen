using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KitchenApp.Models.Exceptions
{
    public class PriceAlreadySetException:Exception
    {
        public PriceAlreadySetException() : base("Price already set for this order!")
        {

        }
    }
}
