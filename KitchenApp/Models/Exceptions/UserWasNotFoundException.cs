using System;

namespace KitchenApp.Models.Exceptions
{
    public class UserWasNotFoundException : Exception
    {
        public UserWasNotFoundException(string login) : base($"User with '{login}' was not found")
        {

        }
    }
}
