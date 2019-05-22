using System;

namespace KitchenApp.Models.Exceptions
{
    public class MenuWasNotFoundException : Exception
    {
        public MenuWasNotFoundException(string name) : base($"Menu with name '{name}' was not found")
        {

        }

        public MenuWasNotFoundException(Guid id) : base($"Menu with Id '{id}' was not found")
        {

        }
    }
}
