using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KitchenApp.Models.Exceptions
{
    public class MenuAleadyIsExsistException:Exception
    {
        public MenuAleadyIsExsistException() : base("Menu is aleady exsist")
        {

        }

    }
}
