using HackerNews.Domain.Models.Articles;
using HackerNews.Domain.Models.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.ViewModels.Users
{
	public class UserSavedView
	{
		public IEnumerable<GetArticleModel> SavedArticles { get; set; }
		public IEnumerable<GetCommentModel> SavedComments { get; set; }
	}
}
