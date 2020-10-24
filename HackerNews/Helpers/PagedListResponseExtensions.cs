using CleanEntityArchitecture.Domain;

namespace HackerNews.Helpers
{
	public static class PagedListResponseExtensions
	{
		public static PagingParams GetNextPagingParams<T>(this PagedListResponse<T> pagedListResponse)
		{
			return pagedListResponse.HasNext
					? new PagingParams
					{
						PageNumber = pagedListResponse.CurrentPage + 1,
						PageSize = pagedListResponse.PageSize
					}
					: new PagingParams
					{
						PageNumber = pagedListResponse.CurrentPage,
						PageSize = pagedListResponse.PageSize
					};
		}

		public static PagingParams GetPrevPagingParams<T>(this PagedListResponse<T> pagedListResponse)
		{
			return pagedListResponse.HasPrevious
					? new PagingParams
					{
						PageNumber = pagedListResponse.CurrentPage - 1,
						PageSize = pagedListResponse.PageSize
					}
					: new PagingParams
					{
						PageNumber = pagedListResponse.CurrentPage,
						PageSize = pagedListResponse.PageSize
					};
		}


	}
}
