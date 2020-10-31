using HackerNews.Domain.Common.Models.Articles;
using HackerNews.Mvc.Models;

namespace HackerNews.Mvc.ViewModels.Home
{
	public class HomeIndexViewModel
	{
		public FrontendPage<GetArticleModel> ArticlePage { get; set; }
		public string SearchTerm { get; set; }
	}
}
