using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace KitchenApp.ViewsModel
{
    public class MenuModel
    {
        [Required(ErrorMessage = "Name is not assignet")]
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid Id { get; set; }
    }
}
