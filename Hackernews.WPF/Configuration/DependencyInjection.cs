using Hackernews.WPF.ApiClients;
using Hackernews.WPF.Services;
using Hackernews.WPF.ViewModels;
using HackerNews.WPF.MessageBus.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hackernews.WPF.Configuration
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddWPF(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddLogging();

			services.AddSingleton<MainWindow>();
			services.AddSingleton<LoginWindow>();

			services.AddHttpClient();

			services.AddHttpClient<IApiClient, ApiClient>(config =>
			{
				// TODO: put in config file
				string baseUrl = "https://localhost:44300/api/";
				config.BaseAddress = new System.Uri(baseUrl);
			});

			services.AddSingleton<IJwtPrincipal, JwtPrincipal>();
			services.AddSingleton<ISignInManager, WpfSignInManager>();
			services.AddSingleton<PrivateUserViewModel>();

			services.AddSingleton<IEventAggregator, EventAggregator>();

			return services;
		}
	}
}
