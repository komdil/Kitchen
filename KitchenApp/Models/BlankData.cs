using KitchenApp.Models;
using System.Linq;

namespace KitchenApp.Models
{
    public static class BlankData
    {
        static void AddDefaultAdminIfDoesNotExists(KitchenAppContext Context)
        {
            if (!Context.GetEntities<Admin>().Any())
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
            }
        }

        public static void CreateBlankData(KitchenAppContext context)
        {
            AddDefaultAdminIfDoesNotExists(context);
            context.SaveChanges();
        }
    }
}
