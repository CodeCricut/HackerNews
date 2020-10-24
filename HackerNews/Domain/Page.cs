using CleanEntityArchitecture.Domain;
using HackerNews.Helpers;
using System.Collections.Generic;

namespace HackerNews.Domain
{
	public class Page<T>
	{
		private readonly PagedListResponse<T> _pagedListResponse;

		public Page(PagedListResponse<T> pagedListResponse)
		{
			_pagedListResponse = pagedListResponse;
			Items = _pagedListResponse.Items;
		}

		public IEnumerable<T> Items { get; set; }

		public bool HasPrev { get => _pagedListResponse.HasPrevious; }
		public bool HasNext { get => _pagedListResponse.HasNext; }

		public PagingParams PrevPagingParams { get => _pagedListResponse.GetPrevPagingParams(); }
		public PagingParams NextPagingParams { get => _pagedListResponse.GetNextPagingParams(); }

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
