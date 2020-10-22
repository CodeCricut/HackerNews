using HackerNews.Domain;
using HackerNews.Domain.Models.Articles;
using HackerNews.Domain.Models.Comments;
using HackerNews.Domain.Models.Users;

namespace HackerNews.ViewModels.Users
{
	public class PublicUserDetailsViewModel 
	{
		public GetPublicUserModel User { get; set; }
		public Page<GetArticleModel> ArticlePage { get; set; }
		public Page<GetCommentModel> CommentPage { get; set; }
	}
}
