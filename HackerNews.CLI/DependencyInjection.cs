using HackerNews.CLI.Configuration;
using HackerNews.CLI.EntityRepository;
using HackerNews.CLI.FileWriters;
using HackerNews.CLI.InclusionConfiguration;
using HackerNews.CLI.Loggers;
using HackerNews.CLI.Requests.GetArticleById;
using HackerNews.CLI.Requests.GetBoards;
using HackerNews.CLI.Requests.PostBoard;
using HackerNews.CLI.Verbs.GetBoardById;
using HackerNews.CLI.Verbs.GetComments;
using HackerNews.CLI.Verbs.GetEntity;
using HackerNews.CLI.Verbs.GetPublicUsers;
using HackerNews.Domain.Common.Models.Articles;
using HackerNews.Domain.Common.Models.Boards;
using HackerNews.Domain.Common.Models.Comments;
using HackerNews.Domain.Common.Models.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HackerNews.CLI
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddCli(this IServiceCollection services, IConfiguration configuration)
		{
			//services.AddSingleton<IGetBoardProcessor, GetBoardProcessor>()
			//	.AddSingleton<IGetVerbProcessor<GetBoardModel, GetBoardsOptions>, GetBoardProcessor>();


			services.AddTransient<IEntityLogger<GetBoardModel>, BoardLogger>()
				.AddTransient<IConfigurableEntityLogger<GetBoardModel, BoardInclusionConfiguration>, ConfigurableBoardLogger>();

			//services.AddSingleton<IGetArticleProcessor, GetArticleProcessor>()
			//	.AddSingleton<IGetVerbProcessor<GetArticleModel, GetArticleOptions>, GetArticleProcessor>();

			services.AddTransient<IEntityLogger<GetArticleModel>, ArticleLogger>()
				.AddTransient<IConfigurableEntityLogger<GetArticleModel, ArticleInclusionConfiguration>, ConfigurableArticleLogger>();

			services.AddSingleton<IGetCommentProcessor, GetCommentProcessor>()
				.AddSingleton<IGetVerbProcessor<GetCommentModel, GetCommentsOptions>, GetCommentProcessor>();
			services.AddTransient<IEntityLogger<GetCommentModel>, CommentLogger>()
				.AddTransient<IConfigurableEntityLogger<GetCommentModel, CommentInclusionConfiguration>, ConfigurableCommentLogger>();

			services.AddSingleton<IGetPublicUserProcessor, GetPublicUserProcessor>()
				.AddSingleton<IGetVerbProcessor<GetPublicUserModel, GetPublicUsersOptions>, GetPublicUserProcessor>();
			services.AddTransient<IEntityLogger<GetPublicUserModel>, PublicUserLogger>()
				.AddTransient<IConfigurableEntityLogger<GetPublicUserModel, PublicUserInclusionConfiguration>, ConfigurablePublicUserLogger>();


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

			services.AddSingleton<IEntityReader<GetBoardModel>, BoardInclusionReader>();
			services.AddSingleton<IEntityReader<GetArticleModel>, ArticleInclusionReader>();
			services.AddSingleton<IEntityReader<GetCommentModel>, CommentInclusionReader>();
			services.AddSingleton<IEntityReader<GetPublicUserModel>, PublicUserInclusionReader>();

			services.AddSingleton<IEntityInclusionReader<BoardInclusionConfiguration, GetBoardModel>, BoardInclusionReader>();
			services.AddSingleton<IEntityInclusionReader<ArticleInclusionConfiguration, GetArticleModel>, ArticleInclusionReader>();
			services.AddSingleton<IEntityInclusionReader<CommentInclusionConfiguration, GetCommentModel>, CommentInclusionReader>();
			services.AddSingleton<IEntityInclusionReader<PublicUserInclusionConfiguration, GetPublicUserModel>, PublicUserInclusionReader>();

			services.AddSingleton<IVerbositySetter, VerbositySetter>();

			services.AddTransient<BoardInclusionConfiguration>();
			services.AddTransient<ArticleInclusionConfiguration>();
			services.AddTransient<CommentInclusionConfiguration>();
			services.AddTransient<PublicUserInclusionConfiguration>();

			services.AddTransient<GetBoardByIdRequestBuilder>();
			services.AddTransient<GetBoardByIdRequestFactory>();

			services.AddTransient<GetBoardsRequestBuilder>();
			services.AddTransient<GetBoardsRequestFactory>();

			services.AddTransient<GetArticleByIdRequestBuilder>();
			services.AddTransient<GetArticleByIdRequestFactory>();

			services.AddTransient<PostBoardRequestBuilder>();
			services.AddTransient<PostBoardRequestFactory>();


			return services;
		}
	}
}
