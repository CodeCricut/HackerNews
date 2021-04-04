using HackerNews.Application.Common.Interfaces;
using HackerNews.Domain.Common;
using HackerNews.Web.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HackerNews.Web
{
	public static class DependencyInjection
	{
		/// <summary>
		/// Add the necessary Web services to the container and configure any necessary options.
		/// </summary>
		/// <param name="services"></param>
		/// <param name="configuration"></param>
		/// <returns></returns>
		public static IServiceCollection AddWeb(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddSingleton<ICurrentUserService, CurrentUserService>();

			return services;
		}
	}
}
