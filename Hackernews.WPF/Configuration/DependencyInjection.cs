using Hackernews.WPF.ApiClients;
using Hackernews.WPF.Factories;
using Hackernews.WPF.Factories.ViewModels;
using Hackernews.WPF.Services;
using Hackernews.WPF.ViewModels;
using HackerNews.WPF.MessageBus.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;

namespace Hackernews.WPF.Configuration
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddWPF(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddLogging();

			services.AddTransient<MainWindow>();
			services.AddTransient<LoginWindow>();
			services.AddTransient<EntityHomeWindow>();
			services.AddTransient<EntityCreationWindow>();

			// Register all vms
			services.Scan(scan =>
				scan.FromCallingAssembly()
					.AddClasses(c => c.AssignableTo<BaseViewModel>()) // 1. Find the concrete vms
					.UsingRegistrationStrategy(RegistrationStrategy.Skip) // 2. Define how to handle duplicates
					  .AsSelf() // 2. Specify which services they are registered as
					  .WithTransientLifetime());  // 3. Set the lifetime for the services

			services.AddSingleton<LoginWindowViewModel>();
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

			services.AddSingleton<IViewManager, ViewManager>();

			services.AddSingleton<AppMessageHandler>();

			services.AddSingleton<ILoadEntityCommandFactoryPrincipal, LoadEntityCommandFactoryPrincipal>();
			services.AddSingleton<ILoadBoardCommandFactory, LoadBoardCommandFactory>();
			services.AddSingleton<ILoadArticleCommandFactory, LoadArticleCommandFactory>();
			services.AddSingleton<ILoadCommentCommandFactory, LoadCommentCommandFactory>();
			services.AddSingleton<ILoadUserCommandFactory, LoadUserCommandFactory>();

			// TODO:
			// hmmmm it will create new instances when injected, but objects created by the same
			// factory instance will have the same dependency references...
			services.AddTransient<IMainWindowVmFactory, MainWindowVmFactory>();
			services.AddTransient<ILoginWindowVmFactory, LoginWindowVmFactory>();
			//services.AddTransient<IBoardCreationVmFactory, BoardCreationVmFactory>();

			return services;
		}
	}
}
