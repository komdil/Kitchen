using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KitchenApp.DateProvider;

namespace KitchenApp.Models.Exceptions
{
    public class MenuWasNotSelectedForTodayException : Exception
    {
        public MenuWasNotSelectedForTodayException() : base("Menu was not selected for today")
        {

        }
    }
}
