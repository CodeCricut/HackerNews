using HackerNews.Application.Common.Models.Articles;
using HackerNews.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Mvc.ViewModels.Users
{
	public class UserArticlesListView
	{
		public FrontendPage<GetArticleModel> ArticlePage { get; set; }
	}
}
