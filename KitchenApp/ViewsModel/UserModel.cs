using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KitchenApp.ViewsModel
{
    public class UserModel
    {
        [Required(ErrorMessage ="Login is not assignet")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Password is not assignet")]
        public string Password { get; set; }
        [Required(ErrorMessage = "FirstName is not assignet")]

       public Guid Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string Errormessage { get; set; }

    }
}
