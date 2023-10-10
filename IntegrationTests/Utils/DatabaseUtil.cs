using SuperHeroAPI.Data;
using SuperHeroAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTests.Utils
{
    internal class DatabaseUtil
    {
        private DatabaseUtil() { }

        public static void Seed(DataContext context)
        {
            var user = new User
            {
                Username = "eduardo",
                Password = BCrypt.Net.BCrypt.HashPassword("senha")
            };
            context.User.Add(user);

            var hero = new SuperHero
            {
                Name = "Spider Man",
                FirstName = "Peter",
                LastName = "Parker",
                Place = "New York",
                User = user,
            };
            context.SuperHeroes.Add(hero);

            context.SaveChanges();
        }
    }
}
