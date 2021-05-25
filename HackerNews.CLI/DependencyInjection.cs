using HackerNews.CLI.Configuration;
using HackerNews.CLI.EntityRepository;
using HackerNews.CLI.FileWriters;
using HackerNews.CLI.InclusionConfiguration;
using HackerNews.CLI.Loggers;
using HackerNews.Domain.Common.Models.Articles;
using HackerNews.Domain.Common.Models.Boards;
using HackerNews.Domain.Common.Models.Comments;
using HackerNews.Domain.Common.Models.Users;
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

		

			services.AddSingleton<IEntityFinder<GetBoardModel>, BoardFinder>();
			services.AddSingleton<IEntityFinder<GetArticleModel>, ArticleFinder>();
			services.AddSingleton<IEntityFinder<GetCommentModel>, CommentFinder>();
			services.AddSingleton<IEntityFinder<GetPublicUserModel>, PublicUserFinder>();


			services.AddTransient<BoardInclusionConfiguration>();
			services.AddTransient<ArticleInclusionConfiguration>();
			services.AddTransient<CommentInclusionConfiguration>();
			services.AddTransient<PublicUserInclusionConfiguration>();

			return services;
		}
	}
}
