using HackerNews.Domain.Common.Models.Articles;
using HackerNews.Domain.Common.Models.Boards;
using HackerNews.Domain.Common.Models.Comments;
using HackerNews.Domain.Common.Models.Users;

namespace HackerNews.Mvc.ViewModels.ViewComponents
{
	public class CommentCardViewModel
	{
		public GetCommentModel Comment { get; set; }
		public GetArticleModel ParentArticle { get; set; }
		public GetCommentModel ParentComment { get; set; }
		public GetBoardModel Board { get; set; }
		public GetPublicUserModel User { get; set; }
	}
}
