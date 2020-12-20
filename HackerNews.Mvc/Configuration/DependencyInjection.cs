using HackerNews.Mvc.Services;
using HackerNews.Mvc.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
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

			services.AddControllersWithViews();
			services.AddScoped<ICookieService, CookieService>();

			services.AddScoped<IUserAuthService, IdentityUserAuthService>();
			services.AddSingleton<IImageFileReader, ImageFileReader>();
			services.AddSingleton<IImageDataHelper, ImageDataHelper>();

			services.AddSingleton<IGenericHttpClient, GenericHttpClient>();
			services.AddSingleton<IApiJwtManager, ApiJwtManager>();

			return services;
		}
	}
}
