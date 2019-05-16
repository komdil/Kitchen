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

                };
                var salt = Helper.SaltGenerate();
                var passwordHash = Helper.HashPassword("Administrator", salt);
                defaultAdmin.Salt = salt;
                defaultAdmin.Password = passwordHash;
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
