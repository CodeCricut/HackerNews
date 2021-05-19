using HackerNews.CLI.InclusionConfiguration;
using HackerNews.CLI.Verbs.GetArticles;
using HackerNews.CLI.Verbs.GetBoards;
using HackerNews.CLI.Verbs.GetComments;
using HackerNews.CLI.Verbs.GetPublicUsers;

namespace HackerNews.CLI.Util
{
	public static class GetVerbOptionsExtensions
	{
		public static BoardInclusionConfiguration GetBoardInclusionConfiguration(this GetBoardsVerbOptions options)
		{
			return new BoardInclusionConfiguration(options.IncludeAllFields)
			{
				IncludeId = options.IncludeId,
				IncludeTitle = options.IncludeTitle,
				IncludeDescription = options.IncludeDescription,
				IncludeCreateDate = options.IncludeCreateDate,
				IncludeCreatorId = options.IncludeCreatorId,
				IncludeModeratorIds = options.IncludeModeratorIds,
				IncludeSubscriberIds = options.IncludeSubscriberIds,
				IncludeArticleIds = options.IncludeArticleIds,
				IncludeDeleted = options.IncludeDeleted,
				IncludeBoardImageId = options.IncludeImageId
			};
		}

		public static ArticleInclusionConfiguration GetArticleInclusionConfiguration(this GetArticlesVerbOptions options)
		{
			return new ArticleInclusionConfiguration()
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
		}

		public static CommentInclusionConfiguration GetCommentInclusionConfiguration(this GetCommentsVerbOptions options)
		{
			return new CommentInclusionConfiguration()
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
		}

		public static PublicUserInclusionConfiguration GetPublicUserInclusionConfiguration(this GetPublicUsersVerbOptions options)
		{
			return new PublicUserInclusionConfiguration()
			{
				IncludeId = options.IncludeId,
				IncludeUsername = options.IncludeUsername,
				IncludeKarma = options.IncludeKarma,
				IncludeArticleIds = options.IncludeArticleIds,
				IncludeCommentIds = options.IncludeCommentIds,
				IncludeJoinDate = options.IncludeJoinDate,
				IncludeDeleted = options.IncludeDeleted,
				IncludeProfileImageId = options.IncludeProfileImageId
			};
		}
	}
}
