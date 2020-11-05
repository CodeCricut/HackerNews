namespace HackerNews.Domain.Common.Models
{
	public class PagingParams
	{
		public PagingParams(int pageNumber, int pageSize)
		{
			PageNumber = pageNumber;
			PageSize = pageSize;
		}

		public PagingParams()
		{
			PageNumber = 1;
			PageSize = 10;
		}

		public int PageNumber { get; set; }
		public int PageSize { get; set; }
	}
}
