using BubberDinner.Domain.Entities;

namespace BubberDinner.Application.Authentication.Common;
	public record AuthenticationResult
	(
		User User,
		string Token
	);

