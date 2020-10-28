using HackerNews.Mvc.Services;
using HackerNews.Mvc.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HackerNews.Mvc.Configuration
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddMvcProject(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
				.AddCookie(opt =>
				{
					opt.LoginPath = "/users/login";
					opt.LogoutPath = "/users/logout";
				});

			services.AddControllersWithViews();
			services.AddScoped<ICookieService, CookieService>();
			services.AddScoped<IJwtSetterService, JwtCookieSetterService>();
			services.AddScoped<IUserAuthService, CookieUserAuthService>();

			return services;
		}
	}
}
