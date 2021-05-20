using CommandLine;
using HackerNews.CLI.Configuration;
using HackerNews.CLI.Loggers;
using HackerNews.CLI.Verbs.GetArticles;
using HackerNews.CLI.Verbs.GetBoards;
using HackerNews.CLI.Verbs.GetComments;
using HackerNews.CLI.Verbs.GetPublicUsers;
using HackerNews.CLI.Verbs.PostArticle;
using HackerNews.CLI.Verbs.PostBoard;
using HackerNews.CLI.Verbs.PostComment;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Extensions.Logging;
using System;
using System.Linq;
using System.Reflection;


namespace HackerNews.CLI
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection RegisterHostedServiceForVerb(this IServiceCollection services, string[] args)
		{
			var types = LoadVerbs();

			Parser.Default.ParseArguments(args, types)
				.WithParsed(o => RegisterHostedService(o, services))
				.WithNotParsed(errors => errors.Output());

			return services;
		}

		private static Type[] LoadVerbs()
		{
			// Load all verb types using Reflection.
			return Assembly.GetExecutingAssembly().GetTypes()
				.Where(t => t.GetCustomAttribute<VerbAttribute>() != null).ToArray();
		}

		private static void RegisterHostedService(object verb, IServiceCollection services)
		{
			switch (verb)
			{
				case GetBoardsOptions b:
					services.AddSingleton(b);
					services.AddHostedService<GetBoardsHostedService>();
					break;
				case GetArticleOptions a:
					services.AddSingleton(a);
					services.AddHostedService<GetArticlesHostedService>();
					break;
				case GetCommentsOptions c:
					services.AddSingleton(c);
					services.AddHostedService<GetCommentsHostedService>();
					break;
				case GetPublicUsersOptions u:
					services.AddSingleton(u);
					services.AddHostedService<GetPublicUsersHostedService>();
					break;
				case PostBoardOptions b:
					services.AddSingleton(b);
					services.AddHostedService<PostBoardHostedService>();
					break;
				case PostArticleOptions a:
					services.AddSingleton(a);
					services.AddHostedService<PostArticleHostedService>();
					break;
				case PostCommentOptions c:
					services.AddSingleton(c);
					services.AddHostedService<PostCommentHostedService>();
					break;
			}
		}

		/// <summary>
		/// Register the Serilog <see cref="ILogger{TCategoryName}"/> dependency and configure it using the <paramref name="configuration"/>.
		/// </summary>
		/// <param name="services"></param>
		/// <param name="configuration"></param>
		/// <returns>The service collection for chaining.</returns>
		public static IServiceCollection AddSerilogLogger(this IServiceCollection services, IConfiguration configuration)
		{
			// LoggingLevelSwitch is used to dynamically change the minimum logging level of logger.
			string defaultLevelStr = configuration.GetSection("Serilog:MinimumLevel:Default")?.Value;
			LogEventLevel defaultLevel = Enum.Parse<LogEventLevel>(defaultLevelStr);

			var levelSwitch = new LoggingLevelSwitch(); 
			levelSwitch.MinimumLevel = defaultLevel; 

			services.AddSingleton(levelSwitch);

			var logger = new LoggerConfiguration()
						.ReadFrom.Configuration(configuration)
						.Enrich.FromLogContext()
						.MinimumLevel.ControlledBy(levelSwitch) // Allows you to dynamically change log level
						.CreateLogger();

			return services.AddLogging(config =>
			{
				config.ClearProviders();
				config.AddProvider(new SerilogLoggerProvider(logger));

				config.SetMinimumLevel(LogLevel.Trace); // This is the absolute minimum level, which can be overriden by Serilog
			});
		}

		/// <summary>
		/// Register a basic console <see cref="Microsoft.Extensions.Logging.ILogger"/> to the service collection.
		/// </summary>
		/// <param name="services"></param>
		/// <param name="configuration"></param>
		/// <returns></returns>
		public static IServiceCollection AddBasicLogger(this IServiceCollection services, IConfiguration configuration)
		{
			return services.AddLogging(logging =>
			{
				logging.ClearProviders();
				logging.AddConsole(config =>
				{
					config.IncludeScopes = false;
					config.TimestampFormat = "hh:mm:ss";
					});
				
				logging.AddConfiguration(configuration.GetSection("Logging"));
			});
		}

	}
}
