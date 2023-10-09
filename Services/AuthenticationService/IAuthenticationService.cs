using SuperHeroAPI.Models;

namespace SuperHeroAPI.Services.AuthenticationService
{
    public interface IAuthenticationService
    {
        public Task<User> signin(string username, string rawPassword);
        public Task<User> signup(string username, string rawPassword);
    }
}
