using CommandLine;
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
using Serilog.Extensions.Logging;
using System;
using System.Linq;
using System.Reflection;

namespace HackerNews.CLI
{
	public static class ServiceCollectionExtensions
	{
		// TODO: If no hosted service could be found for the command, print possible promlems (ie. the list of failure to bind messages)
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
			//load all verb types using Reflection
			return Assembly.GetExecutingAssembly().GetTypes()
				.Where(t => t.GetCustomAttribute<VerbAttribute>() != null).ToArray();
		}

		private static void RegisterHostedService(object verb, IServiceCollection services)
		{
			switch (verb)
			{
				case GetBoardsVerbOptions b:
					services.AddSingleton(b);
					services.AddHostedService<GetBoardsVerb>();
					break;
				case GetArticlesVerbOptions a:
					services.AddSingleton(a);
					services.AddHostedService<GetArticlesVerb>();
					break;
				case GetCommentsVerbOptions c:
					services.AddSingleton(c);
					services.AddHostedService<GetCommentsVerb>();
					break;
				case GetPublicUsersVerbOptions u:
					services.AddSingleton(u);
					services.AddHostedService<GetPublicUsersVerb>();
					break;
				case PostBoardVerbOptions b:
					services.AddSingleton(b);
					services.AddHostedService<PostBoardVerb>();
					break;
				case PostArticleVerbOptions a:
					services.AddSingleton(a);
					services.AddHostedService<PostArticleVerb>();
					break;
				case PostCommentVerbOptions c:
					services.AddSingleton(c);
					services.AddHostedService<PostCommentVerb>();
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
			var logger = new LoggerConfiguration()
						.ReadFrom.Configuration(configuration)
						.Enrich.FromLogContext()
						.CreateLogger();

			return services.AddLogging(config =>
			{
				config.ClearProviders();
				config.AddProvider(new SerilogLoggerProvider(logger));
				string minimumLevel = configuration.GetSection("Serilog:MinimumLevel")?.Value;

				if (!string.IsNullOrEmpty(minimumLevel))
				{
					config.SetMinimumLevel(Enum.Parse<LogLevel>(minimumLevel));
				}
			});
		}
	}
}
