using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace KitchenApp.Models
{
    public class Menu : Entity
    {
        

        public Menu(KitchenAppContext context) : base(context)
        {

        }
        protected Menu() : base()
        {

        }
    
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual List<Order> Orders { get; set; } = new List<Order>();
    }
}