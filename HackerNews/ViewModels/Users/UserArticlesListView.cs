using HackerNews.Domain;
using HackerNews.Domain.Models.Articles;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.ViewModels.Users
{
	public class UserArticlesListView
	{
		public Page<GetArticleModel> ArticlePage { get; set; }
	}
}
