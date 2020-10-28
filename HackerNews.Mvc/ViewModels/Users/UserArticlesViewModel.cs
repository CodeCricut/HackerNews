using HackerNews.Application.Common.Models.Articles;
using HackerNews.Mvc.Models;

namespace HackerNews.Mvc.ViewModels.Users
{
	public class UserArticlesViewModel
	{
		public FrontendPage<GetArticleModel> ArticlePage { get; set; }
	}
}
