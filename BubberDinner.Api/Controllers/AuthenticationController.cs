using BubberDinner.Application.Services.Authentication;
using BubberDinner.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BubberDinner.Api.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authentication)
        {
            _authenticationService = authentication;
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterRequest request)
        {
            var authresult = _authenticationService.Register(
                request.FirstName,
                request.LastName,
                request.Email,
                request.Password
            );

            var response = new AuthenticationResponse(
                authresult.User.Id,
                authresult.User.FirstName,
                authresult.User.LastName,
                authresult.User.Email,
                authresult.Token
            );
            return Ok(response);
        }


        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
            var authresult = _authenticationService.Login(
                request.Email,
                request.Password
            );

            var response = new AuthenticationResponse(
                authresult.User.Id,
                authresult.User.FirstName,
                authresult.User.LastName,
                authresult.User.Email,
                authresult.Token
            );
            return Ok(response);
        }
    }
}

