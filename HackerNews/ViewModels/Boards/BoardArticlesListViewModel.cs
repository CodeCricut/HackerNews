using CleanEntityArchitecture.Domain;
using HackerNews.Domain.Models.Articles;
using System.Collections.Generic;

namespace HackerNews.ViewModels.Boards
{
	public class BoardArticlesListViewModel
	{
		public PagingParams PagingParams { get; set; }
		public IEnumerable<GetArticleModel> Articles { get; set; }
	}
}
