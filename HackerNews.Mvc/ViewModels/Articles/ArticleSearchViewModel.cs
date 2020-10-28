using HackerNews.Application.Common.Models.Articles;
using HackerNews.Mvc.Models;

namespace HackerNews.Mvc.ViewModels.Articles
{
	public class ArticleSearchViewModel
	{
		public string SearchTerm { get; set; }
		public FrontendPage<GetArticleModel> ArticlePage { get; set; }
	}
}
