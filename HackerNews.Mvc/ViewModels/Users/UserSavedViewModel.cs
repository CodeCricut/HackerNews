using HackerNews.Application.Common.Models.Articles;
using HackerNews.Application.Common.Models.Comments;
using HackerNews.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Mvc.ViewModels.Users
{
	public class UserSavedViewModel
	{
		public FrontendPage<GetArticleModel> SavedArticlesPage { get; set; }
		public FrontendPage<GetCommentModel> SavedCommentsPage { get; set; }
	}
}
