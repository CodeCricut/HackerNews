using HackerNews.Application.Common.Models;
using System.Collections.Generic;

namespace HackerNews.Mvc.Models
{
	public class FrontendPage<T>
	{
		private readonly PaginatedList<T> _pagedListResponse;

		public FrontendPage(PaginatedList<T> pagedListResponse)
		{
			_pagedListResponse = pagedListResponse;
			Items = _pagedListResponse.Items;
		}

		public IEnumerable<T> Items { get; set; }

		public bool HasPrev { get => _pagedListResponse.HasPreviousPage; }
		public bool HasNext { get => _pagedListResponse.HasNextPage; }

		public PagingParams PrevPagingParams { get => _pagedListResponse.PreviousPagingParams; }
		public PagingParams NextPagingParams { get => _pagedListResponse.NextPagingParams; }

		public Dictionary<string, string> NextPageQuery
		{
			get =>
				new Dictionary<string, string>
				{
								{"pageNumber", NextPagingParams.PageNumber.ToString() },
								{"pageSize", NextPagingParams.PageSize.ToString() }
				};
		}
		public Dictionary<string, string> PrevPageQuery
		{
			get =>
				new Dictionary<string, string>
				{
								{"pageNumber", PrevPagingParams.PageNumber.ToString() },
								{"pageSize", PrevPagingParams.PageSize.ToString() }
				};
		}
	}
}
