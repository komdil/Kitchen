using KitchenApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
        [Required(ErrorMessage = "Name is not assignet")]
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual List<Order> Orders { get; set; } = new List<Order>();
    }
}