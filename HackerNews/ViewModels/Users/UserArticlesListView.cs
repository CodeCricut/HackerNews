using HackerNews.Domain;
using HackerNews.Domain.Models.Articles;

namespace HackerNews.ViewModels.Users
{
	public class UserArticlesListView
	{
		public Page<GetArticleModel> ArticlePage { get; set; }
	}
}
