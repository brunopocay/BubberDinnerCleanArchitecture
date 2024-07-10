using BubberDinner.Application.Common.Interfaces.Authentication;
using BubberDinner.Application.Common.Interfaces.Persistence;
using BubberDinner.Domain.Entities;
using BubberDinner.Application.Services.Authentication.Common;
using BubberDinner.Domain.Common.Errors;
using ErrorOr;

namespace BubberDinner.Application.Services.Authentication.Commands
{
	public class AuthenticationCommandService : IAuthenticationCommandService
	{
		private readonly IJwtTokenGenerator jwtTokenGenerator;
		private readonly IUserRepository userRepository;

		public AuthenticationCommandService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
		{
			this.jwtTokenGenerator = jwtTokenGenerator;
			this.userRepository = userRepository;
		}
		public ErrorOr<AuthenticationResult> Register(string firstName, string lastName, string email, string password)
		{	
			if(userRepository.GetUserByEmail(email) is not null)
			{
				return Errors.User.DuplicateEmail;
			}

			var user = new User
			{
				FirstName = firstName,
				LastName = lastName,
				Email = email,
				Password = password
			};

			userRepository.Add(user);
			
			Guid userId = Guid.NewGuid();
			var token = jwtTokenGenerator.GenerateToken(user);

			return new AuthenticationResult(user, token);
		}
	}
}
