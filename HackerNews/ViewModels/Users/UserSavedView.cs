using HackerNews.Domain.Models.Articles;
using HackerNews.Domain.Models.Comments;
using System.Collections.Generic;

namespace HackerNews.ViewModels.Users
{
	public class UserSavedView
	{
		public IEnumerable<GetArticleModel> SavedArticles { get; set; }
		public IEnumerable<GetCommentModel> SavedComments { get; set; }
	}
}
