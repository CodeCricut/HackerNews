﻿using HackerNews.ApiConsumer;
using HackerNews.CLI.ProgramServices;
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
				// Builds and starts the host, but will end when CTRL+C is entered.
				// If the CTRL+C to shutdown process is not desired, then use builder.StartAsync()
				//await builder.RunConsoleAsync();

				await host.StartAsync();
			}
			catch (Exception ex)
			{
				var logger = host?.Services.GetService<ILogger<Program>>();
				if (logger != null)
				{
					logger.LogError(ex.Message);
				}
				else
				{
					Console.WriteLine($"FATAL ERROR: {ex.Message}");
				}
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

			services.AddSerilogLogger(configuration);

			AddProgramServices(services);

			services.AddDomain(configuration)
				.AddApiConsumer();
		}

		/// <summary>
		/// Add the main program services responsible for doing things based on CLI args.
		/// </summary>
		private static void AddProgramServices(IServiceCollection services)
		{
			services.AddProgramService<GetVerb, GetVerbOptions>(_args);
		}
	}
}