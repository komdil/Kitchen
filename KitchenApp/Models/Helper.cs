using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace KitchenApp.Models
{
    public static class Helper
    {
        public const string ADMIN_ROLE = "Admin";
        public const string USER_ROLE = "User";
        public const string DATABASE = "KitchenAppDb";
        public const string CONNECTION_STRING = @"Server=DESKTOP-GL612FN\\SQLSERVER;Database=KitchenAppDb;Trusted_Connection=True;";

        public static string HashPassword(string password, string userSalt)
        {
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: Convert.FromBase64String(userSalt),
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
            return hashed;
        }

        public static string SaltGenerate()
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return Convert.ToBase64String(salt);
        }
    }
}
