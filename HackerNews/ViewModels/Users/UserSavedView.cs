using HackerNews.Domain;
using HackerNews.Domain.Models.Articles;
using HackerNews.Domain.Models.Comments;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace HackerNews.ViewModels.Users
{
	public class UserSavedView
	{
		public Page<GetArticleModel> SavedArticlesPage { get; set; }
		public Page<GetCommentModel> SavedCommentsPage { get; set; }
	}
}
