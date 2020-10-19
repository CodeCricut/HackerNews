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
		private readonly PagedListResponse<GetArticleModel> _pagedListResponse;

		public HomeIndexViewModel(PagedListResponse<GetArticleModel> pagedListResponse)
		{
			Articles = pagedListResponse.Items;
			_pagedListResponse = pagedListResponse;
		}

		public IEnumerable<GetArticleModel> Articles { get; set; }
		public bool HasPrev { get => _pagedListResponse.HasPrevious; }
		public bool HasNext { get => _pagedListResponse.HasNext; }
		public PagingParams PrevPagingParams
		{
			get {
				return _pagedListResponse.HasPrevious
					? new PagingParams
					{
						PageNumber = _pagedListResponse.CurrentPage - 1,
						PageSize = _pagedListResponse.PageSize
					}
					: new PagingParams
					{
						PageNumber = _pagedListResponse.CurrentPage,
						PageSize = _pagedListResponse.PageSize
					};
			}
		}
		public PagingParams NextPagingParams
		{
			get
			{
				return _pagedListResponse.HasNext
					? new PagingParams
					{
						PageNumber = _pagedListResponse.CurrentPage + 1,
						PageSize = _pagedListResponse.PageSize
					}
					: new PagingParams
					{
						PageNumber = _pagedListResponse.CurrentPage,
						PageSize = _pagedListResponse.PageSize
					};
			}
		}
	}
}
