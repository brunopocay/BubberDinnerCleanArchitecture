using BubberDinner.Application.Common.Interfaces.Authentication;
using BubberDinner.Application.Common.Interfaces.Persistence;
using BubberDinner.Application.Services.Authentication.Common;
using BubberDinner.Domain.Entities;
using BubberDinner.Domain.Common.Errors;
using ErrorOr;

namespace BubberDinner.Application.Services.Authentication.Queries
{
	public class AuthenticationQueryService : IAuthenticationQueryService
	{
		private readonly IJwtTokenGenerator jwtTokenGenerator;
		private readonly IUserRepository userRepository;

		public AuthenticationQueryService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
		{
			this.jwtTokenGenerator = jwtTokenGenerator;
			this.userRepository = userRepository;
		}
		public ErrorOr<AuthenticationResult> Login(string email, string password)
		{
			if (userRepository.GetUserByEmail(email) is not User user)
			{
				return Errors.Authentication.InvalidCredentials;
			}

			if(user.Password != password)
			{
				return Errors.Authentication.InvalidCredentials;
			}

			var token = jwtTokenGenerator.GenerateToken(user);

			return new AuthenticationResult(user, token);
		}
	}
}
