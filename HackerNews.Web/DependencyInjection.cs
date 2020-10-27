using HackerNews.Api.Configuration;
using HackerNews.Application.Common.Interfaces;
using HackerNews.Web.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HackerNews.Web
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddWeb(this IServiceCollection services, IConfiguration configuration)
		{
			services.Configure<JwtSettings>(options => configuration.GetSection("JwtSettings").Bind(options));
			services.AddScoped<IJwtGeneratorService, JwtGeneratorService>();
			services.AddSingleton<ICurrentUserService, CurrentUserService>();

			return services;
		}
	}
}
