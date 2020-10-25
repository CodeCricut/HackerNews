using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.Application.Common.Models
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

		public int PageNumber { get; }
		public int PageSize { get; }
	}
}
