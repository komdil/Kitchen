using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KitchenApp.Models.Exceptions
{
    public class UserIsAlreadyExsist:Exception
    {
        public UserIsAlreadyExsist(string login):base($"User with login {login} is already exsist ")
        {

        }


    }
}
