using HackerNews.Domain;
using HackerNews.Domain.Models.Articles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.ViewModels
{
	public class ArticlesListViewModel
	{
		public IEnumerable<GetArticleModel>	ArticleModels { get; set; }
	}
}
