using HackerNews.Domain;
using HackerNews.Domain.Models.Articles;

namespace HackerNews.ViewModels.Articles
{
	public class ArticleSearchViewModel
	{
		public string SearchTerm { get; set; }
		public Page<GetArticleModel> ArticlePage { get; set; }
	}
}
