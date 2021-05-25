using HackerNews.CLI.EntityRepository;
using HackerNews.CLI.InclusionConfiguration;
using HackerNews.Domain.Common.Models.Articles;
using HackerNews.Domain.Common.Models.Boards;
using HackerNews.Domain.Common.Models.Comments;
using HackerNews.Domain.Common.Models.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HackerNews.CLI.Domain
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddCliDomain(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddSingleton<IVerbAccessor, VerbAccessor>();

			services.AddSingleton<IEntityReader<GetBoardModel>, BoardInclusionReader>();
			services.AddSingleton<IEntityReader<GetArticleModel>, ArticleInclusionReader>();
			services.AddSingleton<IEntityReader<GetCommentModel>, CommentInclusionReader>();
			services.AddSingleton<IEntityReader<GetPublicUserModel>, PublicUserInclusionReader>();

			services.AddSingleton<IEntityInclusionReader<BoardInclusionConfiguration, GetBoardModel>, BoardInclusionReader>();
			services.AddSingleton<IEntityInclusionReader<ArticleInclusionConfiguration, GetArticleModel>, ArticleInclusionReader>();
			services.AddSingleton<IEntityInclusionReader<CommentInclusionConfiguration, GetCommentModel>, CommentInclusionReader>();
			services.AddSingleton<IEntityInclusionReader<PublicUserInclusionConfiguration, GetPublicUserModel>, PublicUserInclusionReader>();

			services.AddTransient<BoardInclusionConfiguration>();
			services.AddTransient<ArticleInclusionConfiguration>();
			services.AddTransient<CommentInclusionConfiguration>();
			services.AddTransient<PublicUserInclusionConfiguration>();

			return services;
		}
	}
}
