using BubberDinner.Application.Common.Interfaces.Authentication;
using BubberDinner.Application.Common.Interfaces.Persistence;
using BubberDinner.Application.Common.Interfaces.Services;
using BubberDinner.Infrastructure.Authentication;
using BubberDinner.Infrastructure.Persistence;
using BubberDinner.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BubberDinner.Infrastructure;

public static class DependencyInjection
{
	public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
	{
		services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
		services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
		services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
		services.AddScoped<IUserRepository, UserRepository>();
		return services;
	}
}
