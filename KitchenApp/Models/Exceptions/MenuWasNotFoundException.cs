using System;

namespace KitchenApp.Models.Exceptions
{
    public class MenuWasNotFoundException : Exception
    {
        public MenuWasNotFoundException(Guid id) : base($"Menu with name '{id}' was not found")
        {

        }
    }
}
