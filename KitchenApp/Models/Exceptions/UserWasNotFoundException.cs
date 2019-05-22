using System;

namespace KitchenApp.Models.Exceptions
{
    public class UserWasNotFoundException : Exception
    {
        public UserWasNotFoundException(Guid id) : base($"User with id '{id}' was not found")
        {

        }
    }
}
