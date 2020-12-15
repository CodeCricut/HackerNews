using HackerNews.Domain.Common.Models.Articles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Mvc.ViewModels.Articles
{
	public class ArticleEditViewModel
	{
		public int ArticleId { get; set; }
		public PostArticleModel PostArticleModel { get; set; }
	}
}
