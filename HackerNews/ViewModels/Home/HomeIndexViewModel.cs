using HackerNews.Domain;
using HackerNews.Domain.Models.Articles;

namespace HackerNews.ViewModels.Home
{
	public class HomeIndexViewModel
	{
		public Page<GetArticleModel> ArticlePage { get; set; }
		public string SearchTerm { get; set; }
	}
}
