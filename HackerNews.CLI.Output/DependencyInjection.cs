using HackerNews.CLI.Configuration;
using HackerNews.CLI.FileWriters;
using HackerNews.CLI.InclusionConfiguration;
using HackerNews.CLI.Loggers;
using HackerNews.Domain.Common.Models.Articles;
using HackerNews.Domain.Common.Models.Boards;
using HackerNews.Domain.Common.Models.Comments;
using HackerNews.Domain.Common.Models.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Extensions.Logging;
using System;

namespace HackerNews.CLI.Output
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddCliOutput(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddSingleton<IVerbositySetter, VerbositySetter>();

			services.AddSingleton<IJwtLogger, JwtLogger>();

			services.AddTransient<IEntityLogger<GetBoardModel>, BoardLogger>()
			.AddTransient<IConfigurableEntityLogger<GetBoardModel, BoardInclusionConfiguration>, ConfigurableBoardLogger>();

			services.AddTransient<IEntityLogger<GetArticleModel>, ArticleLogger>()
				.AddTransient<IConfigurableEntityLogger<GetArticleModel, ArticleInclusionConfiguration>, ConfigurableArticleLogger>();

			services.AddTransient<IEntityLogger<GetCommentModel>, CommentLogger>()
				.AddTransient<IConfigurableEntityLogger<GetCommentModel, CommentInclusionConfiguration>, ConfigurableCommentLogger>();

			services.AddTransient<IEntityLogger<GetPublicUserModel>, PublicUserLogger>()
				.AddTransient<IConfigurableEntityLogger<GetPublicUserModel, PublicUserInclusionConfiguration>, ConfigurablePublicUserLogger>();

			services.AddSingleton<IFileWriter, FileWriter>();

			services.AddSingleton<IEntityWriter<GetBoardModel>, BoardCsvWriter>();
			services.AddSingleton<IConfigurableEntityWriter<GetBoardModel, BoardInclusionConfiguration>, BoardCsvWriter>();

			services.AddSingleton<IEntityWriter<GetArticleModel>, ArticleCsvWriter>();
			services.AddSingleton<IConfigurableEntityWriter<GetArticleModel, ArticleInclusionConfiguration>, ArticleCsvWriter>();

			services.AddSingleton<IEntityWriter<GetCommentModel>, CommentCsvWriter>();
			services.AddSingleton<IConfigurableEntityWriter<GetCommentModel, CommentInclusionConfiguration>, CommentCsvWriter>();

			services.AddSingleton<IEntityWriter<GetPublicUserModel>, PublicUserCsvWriter>();
			services.AddSingleton<IConfigurableEntityWriter<GetPublicUserModel, PublicUserInclusionConfiguration>, PublicUserCsvWriter>();

			return services;
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
