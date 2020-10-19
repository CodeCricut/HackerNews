using CleanEntityArchitecture.Domain;
using HackerNews.Domain.Models.Articles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.ViewModels.Home
{
	public class HomeIndexViewModel
	{
		public PagedList<GetArticleModel> Articles { get; set; }
		public PagingParams PrevPagingParams
		{
			get {
				return Articles.HasPrevious
					? new PagingParams
					{
						PageNumber = Articles.CurrentPage - 1,
						PageSize = Articles.PageSize
					}
					: new PagingParams
					{
						PageNumber = Articles.CurrentPage,
						PageSize = Articles.PageSize
					};
			}
		}
		public PagingParams NextPagingParams
		{
			get
			{
				return Articles.HasNext
					? new PagingParams
					{
						PageNumber = Articles.CurrentPage + 1,
						PageSize = Articles.PageSize
					}
					: new PagingParams
					{
						PageNumber = Articles.CurrentPage,
						PageSize = Articles.PageSize
					};
			}
		}
	}
}
