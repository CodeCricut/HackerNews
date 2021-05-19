using HackerNews.ApiConsumer;
using HackerNews.CLI.EntityRepository;
using HackerNews.CLI.FileWriters;
using HackerNews.CLI.InclusionConfiguration;
using HackerNews.CLI.Loggers;
using HackerNews.CLI.Verbs.Get;
using HackerNews.CLI.Verbs.Post;
using HackerNews.CLI.Verbs.Register;
using HackerNews.Domain;
using HackerNews.Domain.Common.Models.Articles;
using HackerNews.Domain.Common.Models.Boards;
using HackerNews.Domain.Common.Models.Comments;
using HackerNews.Domain.Common.Models.Users;
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

			// Get Verb
			services.AddSingleton<IGetBoardProcessor, GetBoardProcessor>()
				.AddSingleton<IGetVerbProcessor<GetBoardModel, GetBoardsVerbOptions>, GetBoardProcessor>();
			services.AddSingleton<IEntityLogger<GetBoardModel>, BoardLogger>();

			services.AddSingleton<IGetArticleProcessor, GetArticleProcessor>()
				.AddSingleton<IGetVerbProcessor<GetArticleModel, GetArticlesVerbOptions>, GetArticleProcessor>();
			services.AddSingleton<IEntityLogger<GetArticleModel>, ArticleLogger>();

			services.AddSingleton<IGetCommentProcessor, GetCommentProcessor>()
				.AddSingleton<IGetVerbProcessor<GetCommentModel, GetCommentsVerbOptions>, GetCommentProcessor>();
			services.AddSingleton<IEntityLogger<GetCommentModel>, CommentLogger>();

			services.AddSingleton<IGetPublicUserProcessor, GetPublicUserProcessor>()
				.AddSingleton<IGetVerbProcessor<GetPublicUserModel, GetPublicUsersVerbOptions>, GetPublicUserProcessor>();
			services.AddSingleton<IEntityLogger<GetPublicUserModel>, PublicUserLogger>();
			
			// Register verb
			services.AddSingleton<IJwtLogger, JwtLogger>();

			// Post Verb
			services.AddSingleton<IPostBoardProcessor, PostBoardProcessor>()
				.AddSingleton<IPostVerbProcessor<PostBoardModel, GetBoardModel>, PostBoardProcessor>();

			services.AddSingleton<IPostArticleProcessor, PostArticleProcessor>()
				.AddSingleton<IPostVerbProcessor<PostArticleModel, GetArticleModel>, PostArticleProcessor>();

			services.AddSingleton<IPostCommentProcessor, PostCommentProcessor>()
				.AddSingleton<IPostVerbProcessor<PostCommentModel, GetCommentModel>, PostCommentProcessor>();

			services.AddDomain(configuration)
				.AddApiConsumer();

			services.AddSingleton<IFileWriter, FileWriter>();
			services.AddSingleton<IEntityWriter<GetBoardModel>, BoardCsvWriter>();
			services.AddSingleton<IConfigurableEntityWriter<GetBoardModel, BoardInclusionConfiguration>, BoardCsvWriter>();

			services.AddSingleton<IEntityWriter<GetArticleModel>, ArticleCsvWriter>();
			services.AddSingleton<IConfigurableEntityWriter<GetArticleModel, ArticleInclusionConfiguration>, ArticleCsvWriter>();

			services.AddSingleton<IEntityWriter<GetCommentModel>, CommentCsvWriter>();
			services.AddSingleton<IConfigurableEntityWriter<GetCommentModel, CommentInclusionConfiguration>, CommentCsvWriter>();

			services.AddSingleton<IEntityWriter<GetPublicUserModel>, PublicUserCsvWriter>();
			services.AddSingleton<IConfigurableEntityWriter<GetPublicUserModel, PublicUserInclusionConfiguration>, PublicUserCsvWriter>();

			services.AddSingleton<IGetEntityRepository<GetBoardModel>, GetBoardRepository>();
			services.AddSingleton<IGetEntityRepository<GetArticleModel>, GetArticleRepository>();
			services.AddSingleton<IGetEntityRepository<GetCommentModel>, GetCommentRepository>();
			services.AddSingleton<IGetEntityRepository<GetPublicUserModel>, GetPublicUserRepository>();

			AddProgramServices(services);
		}

		/// <summary>
		/// Add the main program services responsible for doing things based on CLI args.
		/// </summary>
		private static void AddProgramServices(IServiceCollection services)
		{
			services.AddProgramService<GetBoardsVerb, GetBoardsVerbOptions>(_args);
			services.AddProgramService<GetArticlesVerb, GetArticlesVerbOptions>(_args);
			services.AddProgramService<GetCommentsVerb, GetCommentsVerbOptions>(_args);
			services.AddProgramService<GetPublicUsersVerb, GetPublicUsersVerbOptions>(_args);

			services.AddProgramService<PostVerb, PostVerbOptions>(_args);
			services.AddProgramService<RegisterVerb, RegisterVerbOptions>(_args);
		}
	}
}
