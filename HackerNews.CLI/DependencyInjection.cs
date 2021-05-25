using HackerNews.CLI.Loggers;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace HackerNews.CLI
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddCli(this IServiceCollection services, IConfiguration configuration)
		{
			//services.AddSingleton<IGetBoardProcessor, GetBoardProcessor>()
			//	.AddSingleton<IGetVerbProcessor<GetBoardModel, GetBoardsOptions>, GetBoardProcessor>();

			services.AddMediatR(Assembly.GetExecutingAssembly());

			services.AddSingleton<IJwtLogger, JwtLogger>();

			//services.AddSingleton<IPostBoardProcessor, PostBoardProcessor>()
			//	.AddSingleton<IPostVerbProcessor<PostBoardModel, GetBoardModel, PostBoardOptions>,
			//		PostBoardProcessor>();

			//services.AddSingleton<IPostArticleProcessor, PostArticleProcessor>()
			//	.AddSingleton<IPostVerbProcessor<PostArticleModel, GetArticleModel, PostArticleOptions>,
			//		PostArticleProcessor>();

			//services.AddSingleton<IPostCommentProcessor, PostCommentProcessor>()
			//	.AddSingleton<IPostVerbProcessor<PostCommentModel, GetCommentModel, PostCommentOptions>,
			//		PostCommentProcessor>();





			return services;
		}
	}
}
