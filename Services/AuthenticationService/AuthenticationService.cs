using Microsoft.EntityFrameworkCore;
using SuperHeroAPI.Data;
using SuperHeroAPI.Exceptions;
using SuperHeroAPI.Models;

namespace SuperHeroAPI.Services.AuthenticationService
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly DataContext context;

        public AuthenticationService(DataContext context)
        {
            this.context = context;
        }

        public async Task<User> signin(string username, string rawPassword)
        {
            User? user = await context.User.Where(user => user.Username == username).FirstOrDefaultAsync();
            if (user == null)
            {
                throw new UserNotFoundException();
            }

            if (!BCrypt.Net.BCrypt.Verify(rawPassword, user.Password))
            {
                throw new PasswordMismatchException();
            }

            return user;
        }

        public async Task<User> signup(string username, string rawPassword)
        {
            bool alreadyExists = await context.User.Where(user => user.Username == username).AnyAsync();
            if (alreadyExists)
            {
                throw new UsernameAlreadyTakenException();
            }

            User user = new User
            {
                Username = username,
                Password = BCrypt.Net.BCrypt.HashPassword(rawPassword)
            };

            context.User.Add(user);
            await context.SaveChangesAsync();

            return user;
        }
    }
}
