using HackerNews.CLI.InclusionConfiguration;
using HackerNews.CLI.Options;

namespace HackerNews.CLI.Requests.Configuration
{
	public static class CommentInclusionRequestConfigurationExtensions
	{
		public static TBaseRequestBuilder FromOptions<TBaseRequestBuilder, TRequest>(
			this ICommentInclusionRequestConfiguration<TBaseRequestBuilder, TRequest> commentInclsuionConfig,
			ICommentInclusionOptions options)
			where TBaseRequestBuilder : IRequestBuilder<TRequest>
		{
			CommentInclusionConfiguration inclusionConfig;
			if (options.IncludeAllFields)
				inclusionConfig = new CommentInclusionConfiguration(true);
			else
				inclusionConfig = new CommentInclusionConfiguration()
				{

					IncludeId = options.IncludeId,
					IncludeUserId = options.IncludeUserId,
					IncludeText = options.IncludeText,
					IncludeUrl = options.IncludeUrl,
					IncludeKarma = options.IncludeKarma,
					IncludeCommentIds = options.IncludeCommentIds,
					IncludeParentCommentId = options.IncludeParentCommentId,
					IncludeParentArticleId = options.IncludeParentArticleId,
					IncludeDeleted = options.IncludeDeleted,
					IncludeUsersLiked = options.IncludeUsersLiked,
					IncludeUsersDisliked = options.IncludeUsersDisliked,
					IncludePostDate = options.IncludePostDate,
					IncludeBoardId = options.IncludeBoardId
				};

			commentInclsuionConfig.CommentInclusionConfiguration = inclusionConfig;

			return commentInclsuionConfig.BaseRequest;
		}
	}
}
