using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace KitchenApp.ViewsModel
{
    public class MenuModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name is not assignet")]
        public string Name { get; set; }
        public string Description { get; set; }
      
        public string ErrorMessage { get; set; }
    }
}
