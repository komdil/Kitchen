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
        public static string ConnectionString { get; set; } = "Server=DESKTOP-GL612FN\\SQLSERVER;Database=KitchenAppDb;Trusted_Connection=True;";
    }
}
