using HackerNews.CLI.InclusionConfiguration;
using HackerNews.CLI.Verbs.Get;

namespace HackerNews.CLI.Util
{
	public static class GetVerbOptionsExtensions
	{
		public static BoardInclusionConfiguration GetBoardInclusionConfiguration(this GetVerbOptions options)
		{
			return new BoardInclusionConfiguration(options.IncludeAllFields)
			{
				IncludeId = options.IncludeBoardId,
				IncludeTitle = options.IncludeBoardTitle,
				IncludeDescription = options.IncludeBoardDescription,
				IncludeCreateDate = options.IncludeBoardCreateDate,
				IncludeCreatorId = options.IncludeBoardCreatorId,
				IncludeModeratorIds = options.IncludeBoardModeratorIds,
				IncludeSubscriberIds = options.IncludeBoardSubscriberIds,
				IncludeArticleIds = options.IncludeBoardArticleIds,
				IncludeDeleted = options.IncludeBoardDeleted,
				IncludeBoardImageId = options.IncludeBoardImageId
			};
		}

		public static ArticleInclusionConfiguration GetArticleInclusionConfiguration(this GetVerbOptions options)
		{
			return new ArticleInclusionConfiguration()
			{
				// TODO
			};
		}

		public static CommentInclusionConfiguration GetCommentInclusionConfiguration(this GetVerbOptions options)
		{
			return new CommentInclusionConfiguration()
			{
				IncludeId				= options.IncludeCommentId,
				IncludeUserId			= options.IncludeCommentUserId,
				IncludeText				= options.IncludeCommentText,
				IncludeUrl				= options.IncludeCommentUrl,
				IncludeKarma			= options.IncludeCommentKarma,
				IncludeCommentIds		= options.IncludeCommentCommentIds,
				IncludeParentCommentId	= options.IncludeCommentParentCommentId,
				IncludeParentArticleId	= options.IncludeCommentParentArticleId,
				IncludeDeleted			= options.IncludeCommentDeleted,
				IncludeUsersLiked		= options.IncludeCommentUsersLiked,
				IncludeUsersDisliked	= options.IncludeCommentUsersDisliked,
				IncludePostDate			= options.IncludeCommentPostDate,
				IncludeBoardId			= options.IncludeCommentBoardId
			};
		}

		public static PublicUserInclusionConfiguration GetPublicUserInclusionConfiguration(this GetVerbOptions options)
		{
			return new PublicUserInclusionConfiguration()
			{
				// TODO
			};
		}
	}
}
