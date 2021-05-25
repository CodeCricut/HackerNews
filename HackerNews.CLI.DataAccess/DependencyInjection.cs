using HackerNews.CLI.EntityRepository;
using HackerNews.Domain.Common.Models.Articles;
using HackerNews.Domain.Common.Models.Boards;
using HackerNews.Domain.Common.Models.Comments;
using HackerNews.Domain.Common.Models.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HackerNews.CLI.DataAccess
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddCliDataAccess(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddSingleton<IEntityFinder<GetBoardModel>, BoardFinder>();
			services.AddSingleton<IEntityFinder<GetArticleModel>, ArticleFinder>();
			services.AddSingleton<IEntityFinder<GetCommentModel>, CommentFinder>();
			services.AddSingleton<IEntityFinder<GetPublicUserModel>, PublicUserFinder>();

			return services;
		}
	}
}
