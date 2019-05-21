using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KitchenApp.Models;

namespace KitchenApp.Models.Exceptions
{
    public class MenuAlreadySelectedException : Exception
    {
        public MenuAlreadySelectedException() : base("You have been already choosed menu for today!")
        {

        }
    }
}
