using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KitchenApp.ViewsModel
{
    public class LoginModel
    {

        [Required(ErrorMessage = "Login is not assignet")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Password is not assignet")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
