using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KitchenApp.Models
{
    public static class Helper
    {
        public const string ADMIN_ROLE = "Admin";
        public const string USER_ROLE = "User";
        public const string DATABASE = "KitchenAppDb";
        public const string CONNECTION_STRING = "Server=(localdb)\\MSSQLLocalDB;Database=KitchenAppDb;Trusted_Connection=True;";
        public static Guid IdMenu;
    }
}
