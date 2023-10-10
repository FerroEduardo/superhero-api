using SuperHeroAPI.Data;
using SuperHeroAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTests
{
    internal class DatabaseUtil
    {
        private DatabaseUtil() { }

        public static void Seed(DataContext context)
        {
            User user = new User
            {
                Username = "eduardo",
                Password = BCrypt.Net.BCrypt.HashPassword("senha")
            };
            context.User.Add(user);

            context.SaveChanges();
        }
    }
}
