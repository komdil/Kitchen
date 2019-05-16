using KitchenApp.DateProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KitchenApp.Models
{
    public class Menu : Entity
    {
        public Menu(KitchenAppContext context) : base(context)
        {

        }
        public Menu() : base()
        {
            
        }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual List<Order> Orders { get; set; } = new List<Order>();
    }
}