using Cloudbass.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Cloudbass.Database
{
    public static class CloudbassSeedData
    {
        public static void EnsureSeedData(this CloudbassContext db)
        {
            if (!db.Users.Any())
            {
                string salt = Guid.NewGuid().ToString("N");

                using var sha = SHA512.Create();
                byte[] hash = sha.ComputeHash(Encoding.UTF8.GetBytes("Cloudba55" + salt));

                var users = new List<User>
                {
                    new User

                    {   Name = "Admin",
                        Password= Convert.ToBase64String(hash),
                        Salt = salt
                    }
                };

                db.Users.AddRange(users);
                db.SaveChanges();
            }

        }
    }
}
