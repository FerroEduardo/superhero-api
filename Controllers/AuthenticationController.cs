using Microsoft.AspNetCore.Mvc;
using SuperHeroAPI.Models.Request;
using SuperHeroAPI.Services;
using SuperHeroAPI.Services.AuthenticationService;

namespace SuperHeroAPI.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly TokenService tokenService;
        private readonly IAuthenticationService authenticationService;

        public AuthenticationController(TokenService tokenService, IAuthenticationService authenticationService)
        {
            this.tokenService = tokenService;
            this.authenticationService = authenticationService;
        }

        [HttpPost("signin")]
        public async Task<ActionResult<string>> signin([FromBody] SigninRequest request)
        {
            var user = await authenticationService.signin(request.Username, request.Password);
            var token = tokenService.GenerateToken(user.Id);

            return Ok(token);
        }

        [HttpPost("signup")]
        public async Task<ActionResult<string>> signup([FromBody] SigninRequest request)
        {
            var user = await authenticationService.signup(request.Username, request.Password);
            var token = tokenService.GenerateToken(user.Id);

            return Ok(token);
        }
    }
}
