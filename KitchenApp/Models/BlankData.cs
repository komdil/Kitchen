using KitchenApp.DateProvider;
using System.Linq;

namespace KitchenApp.Models
{
    public static class BlankData
    {
        static void AddDefaultAdminIfDoesNotExists(KitchenAppContext Context)
        {
            if (!Context.Admins.Any())
            {
                Admin defaultAdmin = new Admin(Context)
                {
                    FirstName = "Administrator",
                    LastName = "Administrator",
                    Login = "Administrator",
                    Password = "Administrator",
                };
                Context.Admins.Add(defaultAdmin);
            }
        }

        public static void CreateBlankData(KitchenAppContext context)
        {
            AddDefaultAdminIfDoesNotExists(context);
            context.SaveChanges();
        }
    }
}
