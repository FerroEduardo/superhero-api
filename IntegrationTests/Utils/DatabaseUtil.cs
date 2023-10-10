using SuperHeroAPI.Data;
using SuperHeroAPI.Models;

namespace IntegrationTests.Utils
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
