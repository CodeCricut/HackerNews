using Microsoft.Extensions.DependencyInjection;
using HackerNews.CLI.EntityRepository;
using HackerNews.CLI.FileWriters;
using HackerNews.CLI.InclusionConfiguration;
using HackerNews.CLI.Loggers;
using HackerNews.CLI.Verbs.GetArticles;
using HackerNews.CLI.Verbs.GetBoards;
using HackerNews.CLI.Verbs.GetComments;
using HackerNews.CLI.Verbs.GetEntity;
using HackerNews.CLI.Verbs.GetPublicUsers;
using HackerNews.CLI.Verbs.Post;
using HackerNews.CLI.Verbs.PostArticle;
using HackerNews.CLI.Verbs.PostBoard;
using HackerNews.CLI.Verbs.PostComment;
using HackerNews.Domain.Common.Models.Articles;
using HackerNews.Domain.Common.Models.Boards;
using HackerNews.Domain.Common.Models.Comments;
using HackerNews.Domain.Common.Models.Users;
using Microsoft.Extensions.Configuration;
using HackerNews.CLI.Configuration;

namespace HackerNews.CLI
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddCli(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddSingleton<IGetBoardProcessor, GetBoardProcessor>()
				.AddSingleton<IGetVerbProcessor<GetBoardModel, GetBoardsOptions>, GetBoardProcessor>();


			services.AddSingleton<IEntityLogger<GetBoardModel>, BoardLogger>()
				.AddSingleton<IConfigurableEntityLogger<GetBoardModel, BoardInclusionConfiguration>, BoardLogger>();

			services.AddSingleton<IGetArticleProcessor, GetArticleProcessor>()
				.AddSingleton<IGetVerbProcessor<GetArticleModel, GetArticleOptions>, GetArticleProcessor>();

			services.AddSingleton<IEntityLogger<GetArticleModel>, ArticleLogger>()
				.AddSingleton<IConfigurableEntityLogger<GetArticleModel, ArticleInclusionConfiguration>, ArticleLogger>();

			services.AddSingleton<IGetCommentProcessor, GetCommentProcessor>()
				.AddSingleton<IGetVerbProcessor<GetCommentModel, GetCommentsOptions>, GetCommentProcessor>();
			services.AddSingleton<IEntityLogger<GetCommentModel>, CommentLogger>()
				.AddSingleton<IConfigurableEntityLogger<GetCommentModel, CommentInclusionConfiguration>, CommentLogger>();

			services.AddSingleton<IGetPublicUserProcessor, GetPublicUserProcessor>()
				.AddSingleton<IGetVerbProcessor<GetPublicUserModel, GetPublicUsersOptions>, GetPublicUserProcessor>();
			services.AddSingleton<IEntityLogger<GetPublicUserModel>, PublicUserLogger>()
				.AddSingleton<IConfigurableEntityLogger<GetPublicUserModel, PublicUserInclusionConfiguration>, PublicUserLogger>();


			services.AddSingleton<IJwtLogger, JwtLogger>();

			services.AddSingleton<IPostBoardProcessor, PostBoardProcessor>()
				.AddSingleton<IPostVerbProcessor<PostBoardModel, GetBoardModel, PostBoardOptions>,
					PostBoardProcessor>();

			services.AddSingleton<IPostArticleProcessor, PostArticleProcessor>()
				.AddSingleton<IPostVerbProcessor<PostArticleModel, GetArticleModel, PostArticleOptions>,
					PostArticleProcessor>();

			services.AddSingleton<IPostCommentProcessor, PostCommentProcessor>()
				.AddSingleton<IPostVerbProcessor<PostCommentModel, GetCommentModel, PostCommentOptions>,
					PostCommentProcessor>();

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

			services.AddSingleton<IEntityInclusionReader<BoardInclusionConfiguration, GetBoardModel>, BoardInclusionReader>();
			services.AddSingleton<IEntityInclusionReader<ArticleInclusionConfiguration, GetArticleModel>, ArticleInclusionReader>();
			services.AddSingleton<IEntityInclusionReader<CommentInclusionConfiguration, GetCommentModel>, CommentInclusionReader>();
			services.AddSingleton<IEntityInclusionReader<PublicUserInclusionConfiguration, GetPublicUserModel>, PublicUserInclusionReader>();


			services.AddSingleton<IVerbositySetter, VerbositySetter>();
			return services;
		}
	}
}
