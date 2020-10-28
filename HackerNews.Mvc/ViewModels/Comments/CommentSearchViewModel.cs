using HackerNews.Application.Common.Models.Comments;
using HackerNews.Mvc.Models;

namespace HackerNews.Mvc.ViewModels.Comments
{
	public class CommentSearchViewModel
	{
		public string SearchTerm { get; set; }
		public FrontendPage<GetCommentModel> CommentPage { get; set; }
	}
}
