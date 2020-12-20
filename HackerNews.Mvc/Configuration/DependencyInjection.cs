using HackerNews.Mvc.Services;
using HackerNews.Mvc.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HackerNews.Mvc.Configuration
{
	public static class DependencyInjection
	{
		/// <summary>
		/// Register MVC specific services to the container.
		/// </summary>
		/// <param name="services"></param>
		/// <param name="configuration"></param>
		/// <returns></returns>
		public static IServiceCollection AddMvcProject(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddAuthentication(defaultScheme: CookieAuthenticationDefaults.AuthenticationScheme)
				.AddCookie();

			services.PostConfigure<CookieAuthenticationOptions>(IdentityConstants.ApplicationScheme,
				opt =>
				{
					//configure your other properties
					opt.AccessDeniedPath = "/Users/Login";
					opt.LoginPath = "/Users/Login";
				});

			services.AddControllersWithViews(opt =>
			{
				// Optionally require authorization by default on all actions
				// If you go this route, decorate actions such as Login with [AllowAnonymous]
				//var policy = new AuthorizationPolicyBuilder()
				//	.RequireAuthenticatedUser()
				//	.Build();
				//opt.Filters.Add(new AuthorizeFilter(policy));
			});
			services.AddScoped<ICookieService, CookieService>();

			services.AddScoped<IUserAuthService, IdentityUserAuthService>();
			services.AddSingleton<IImageFileReader, ImageFileReader>();
			services.AddSingleton<IImageDataHelper, ImageDataHelper>();

			services.AddScoped<IGenericHttpClient, GenericHttpClient>();
			services.AddScoped<IApiJwtManager, ApiJwtManager>();
			services.AddScoped<IApiJwtCookieService, ApiJwtCookieService>();

			return services;
		}
	}
}
