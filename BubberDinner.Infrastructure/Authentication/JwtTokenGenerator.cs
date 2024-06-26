using BubberDinner.Application.Common.Interfaces.Authentication;
using BubberDinner.Application.Common.Interfaces.Services;
using BubberDinner.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BubberDinner.Infrastructure.Authentication
{
	public class JwtTokenGenerator : IJwtTokenGenerator
	{
		private readonly IDateTimeProvider dateTimeProvider;
		private readonly JwtSettings jwtSettings;

		public JwtTokenGenerator(IDateTimeProvider dateTimeProvider, IOptions<JwtSettings> jwtOptions)
		{
			this.dateTimeProvider = dateTimeProvider;
			this.jwtSettings = jwtOptions.Value;
		}

		public string GenerateToken(User user)
		{
			var signingCredentials = new SigningCredentials(
				new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
				SecurityAlgorithms.HmacSha256 );


			var claims = new[]
			{
				new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
				new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
				new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
			};

			var securityToken = new JwtSecurityToken(
				issuer: "BubberDinner",
				audience: jwtSettings.Audience,
				expires: dateTimeProvider.UtcNow.AddMinutes(jwtSettings.ExpiryMinutes),
				claims: claims,
				signingCredentials: signingCredentials);

			return new JwtSecurityTokenHandler().WriteToken(securityToken);
		}
	}
}
