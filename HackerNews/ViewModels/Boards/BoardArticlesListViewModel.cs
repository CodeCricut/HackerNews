using HackerNews.Domain.Models.Articles;
using HackerNews.Domain.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.ViewModels.Boards
{
	public class BoardArticlesListViewModel
	{
		public PagingParams	PagingParams { get; set; }
		public IEnumerable<GetArticleModel> Articles { get; set; }
	}
}
