using HackerNews.Domain.Common.Models.Articles;

namespace HackerNews.Mvc.ViewModels.Articles
{
	public class ArticleEditViewModel
	{
		public int ArticleId { get; set; }
		public PostArticleModel PostArticleModel { get; set; }
	}
}
