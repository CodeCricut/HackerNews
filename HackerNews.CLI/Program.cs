using HackerNews.ApiConsumer;
using HackerNews.Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;

namespace HackerNews.CLI
{
	// TODO:
	// fix --all flag for Get verbs
	// add -v --verbose flag
	class Program
	{
		private static string[] _args = new string[0];
		static async Task<int> Main(string[] args)
		{
			_args = args;

			IHostBuilder builder = BuildHostBuilder();

			using IHost host = builder.Build();
			try
			{
				var logger = host?.Services.GetService<ILogger<Program>>();
				logger.LogTrace("Starting host...");
				await host.StartAsync();
				logger.LogTrace("Host started.");
			}
			catch (Exception ex)
			{
				var logger = host?.Services.GetService<ILogger<Program>>();
				if (logger != null)
					logger.LogError(ex.Message);
				else
					Console.WriteLine($"FATAL ERROR: {ex.Message}");
				return 1;
			}

			return 0;
		}

		/// <summary>
		/// Build a host responsible for encapsulating app resources such as:
		///		*	- services
		///		*	- logging
		///		*	- configuration
		///		*	- hosted services(where hosted services are tasks that have an entry and exit procedure)
		/// </summary>
		/// <returns></returns>
		private static IHostBuilder BuildHostBuilder()
		{
			var builder = new HostBuilder()
				.ConfigureAppConfiguration(Configure)
				.ConfigureServices(ConfigureServices);
			return builder;
		}

		/// <summary>
		/// Set up the configuration for the host. This is where you should configure things such as files and environment
		/// variables to be included.
		/// </summary>
		private static void Configure(IConfigurationBuilder configBuilder)
		{
			configBuilder.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile(AppDomain.CurrentDomain.BaseDirectory + "appsettings.json", optional: false, reloadOnChange: true)
				.AddEnvironmentVariables();
		}

		/// <summary>
		/// Register services for the host.
		/// </summary>
		private static void ConfigureServices(HostBuilderContext hostContext, IServiceCollection services)
		{
			// Configure app to use a custom shutdown timeout
			services.Configure((Action<HostOptions>)(option =>
			{
				// Default is 5; If shutdown is requested and hosted services don't stop before the timeout, they will be forced to stop
				option.ShutdownTimeout = System.TimeSpan.FromSeconds(3);
			}));

			// The host has access to the configuration.
			var configuration = hostContext.Configuration;


			services.AddDomain(configuration)
				.AddApiConsumer(configuration)
				.AddCli(configuration);

			services.AddSerilogLogger(configuration);
			//services.AddBasicLogger(configuration);

			services.RegisterHostedServiceForVerb(_args);
		}
	}
}
