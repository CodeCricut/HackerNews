using HackerNews.CLI.InclusionConfiguration;
using HackerNews.CLI.Options;

namespace HackerNews.CLI.Requests.Configuration
{
	public static class ArticleInclusionRequestConfigurationExtensions
	{
		public static TBaseRequestBuilder FromOptions<TBaseRequestBuilder, TRequest>(
			this IArticleInclusionRequestConfiguration<TBaseRequestBuilder, TRequest> articleInclusionConfig,
			IArticleInclusionOptions options)
			where TBaseRequestBuilder : IRequestBuilder<TRequest>
		{
			ArticleInclusionConfiguration inclusionConfig;
			if (options.IncludeAllFields)
				inclusionConfig = new ArticleInclusionConfiguration(true);
			else
				inclusionConfig = new ArticleInclusionConfiguration()
				{
					IncludeId = options.IncludeId,
					IncludeType = options.IncludeType,
					IncludeUserId = options.IncludeUserId,
					IncludeText = options.IncludeText,
					IncludeCommentIds = options.IncludeCommentIds,
					IncludeKarma = options.IncludeKarma,
					IncludeTitle = options.IncludeTitle,
					IncludeUsersLiked = options.IncludeUsersLiked,
					IncludeUsersDisliked = options.IncludeUsersDisliked,
					IncludePostDate = options.IncludePostDate,
					IncludeBoardId = options.IncludeBoardId,
					IncludeDeleted = options.IncludeDeleted,
					IncludeAssociatedImageId = options.IncludeAssociatedImageId
				};

			articleInclusionConfig.ArticleInclusionConfiguration = inclusionConfig;

			return articleInclusionConfig.BaseRequest;
		}
	}
}
