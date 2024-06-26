using BubberDinner.Application.Common.Interfaces.Authentication;
using BubberDinner.Application.Common.Interfaces.Persistence;
using BubberDinner.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BubberDinner.Application.Services.Authentication
{
	public class AuthenticationService : IAuthenticationService
	{
		private readonly IJwtTokenGenerator jwtTokenGenerator;
		private readonly IUserRepository userRepository;

		public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
		{
			this.jwtTokenGenerator = jwtTokenGenerator;
			this.userRepository = userRepository;
		}

		public AuthenticationResult Login(string email, string password)
		{
			if (userRepository.GetUserByEmail(email) is not User user) 
			{
				throw new Exception("User with given email does not exists.");
			}

			if(user.Password != password)
			{
				throw new Exception("Invalid Email/Password");
			}

			var token = jwtTokenGenerator.GenerateToken(user);

			return new AuthenticationResult(user, token);
		}

		public AuthenticationResult Register(string firstName, string lastName, string email, string password)
		{	
			if(userRepository.GetUserByEmail(email) is not null)
			{
				throw new Exception("User with given email already exists.");
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
