using HackerNews.CLI.InclusionConfiguration;
using HackerNews.CLI.Verbs.Get;

namespace HackerNews.CLI.Util
{
	public static class GetVerbOptionsExtensions
	{
		public static BoardInclusionConfiguration GetBoardInclusionConfiguration(this GetBoardsVerbOptions options)
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

		public static ArticleInclusionConfiguration GetArticleInclusionConfiguration(this GetArticlesVerbOptions options)
		{
			return new ArticleInclusionConfiguration()
			{
				IncludeId					= options.IncludeArticleId,
				IncludeType					= options.IncludeArticleType,
				IncludeUserId				= options.IncludeArticleUserId,
				IncludeText					= options.IncludeArticleText,
				IncludeCommentIds			= options.IncludeArticleCommentIds,
				IncludeKarma				= options.IncludeArticleKarma,
				IncludeTitle				= options.IncludeArticleTitle,
				IncludeUsersLiked			= options.IncludeArticleUsersLiked,
				IncludeUsersDisliked		= options.IncludeArticleUsersDisliked,
				IncludePostDate				= options.IncludeArticlePostDate,
				IncludeBoardId				= options.IncludeArticleBoardId,
				IncludeDeleted				= options.IncludeArticleDeleted,
				IncludeAssociatedImageId	= options.IncludeArticleAssociatedImageId
			};
		}

		public static CommentInclusionConfiguration GetCommentInclusionConfiguration(this GetCommentsVerbOptions options)
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

		public static PublicUserInclusionConfiguration GetPublicUserInclusionConfiguration(this GetPublicUsersVerbOptions options)
		{
			return new PublicUserInclusionConfiguration()
			{
				IncludeId				= options.IncludeUserId,
				IncludeUsername			= options.IncludeUserUsername,
				IncludeKarma			= options.IncludeUserKarma,
				IncludeArticleIds		= options.IncludeUserArticleIds,
				IncludeCommentIds		= options.IncludeUserCommentIds,
				IncludeJoinDate			= options.IncludeUserJoinDate,
				IncludeDeleted			= options.IncludeUserDeleted,
				IncludeProfileImageId	= options.IncludeUserProfileImageId
			};
		}
	}
}
