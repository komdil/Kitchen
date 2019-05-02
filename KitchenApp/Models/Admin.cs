using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KitchenApp.Models
{
    public class Admin:User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }


    }
}
