using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KitchenApp.ViewsModel
{
    public class MenuModel
    {
       
        [Required(ErrorMessage ="Name is not assignet")]
        public string Name { get; set; }
        public string Description { get; set; }

    }
}
