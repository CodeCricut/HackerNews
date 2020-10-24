using HackerNews.Domain;
using HackerNews.Domain.Models.Comments;

namespace HackerNews.ViewModels.Comments
{
	public class CommentSearchViewModel
	{
		public string SearchTerm { get; set; }
		public Page<GetCommentModel> CommentPage { get; set; }
	}
}
