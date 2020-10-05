using HackerNews.Domain.Models.Articles;
using System.Collections.Generic;

namespace HackerNews.ViewModels
{
	public class ArticlesListViewModel
	{
		public IEnumerable<GetArticleModel> ArticleModels { get; set; }
	}
}
