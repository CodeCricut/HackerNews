using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Domain.Common.Models
{
	/// <summary>
	/// A portion of a potentially larger list of <typeparamref name="T"/>. 
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class PaginatedList<T>
	{
		public List<T> Items { get; set; } = new List<T>();
		public int PageIndex { get; set; }
		public int TotalPages { get; set; }
		public int TotalCount { get; set; }
		public int PageSize { get; set; }

		// For deserialization
		public PaginatedList()
		{

		}

		public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
		{
			PageIndex = pageIndex;
			TotalPages = (int)Math.Ceiling(count / (double)pageSize);
			TotalCount = count;
			Items = items;
			PageSize = pageSize;
		}

		public bool HasPreviousPage => PageIndex > 1;

		public bool HasNextPage => PageIndex < TotalPages;

		public PagingParams PreviousPagingParams
		{
			get
			{
				var pageNumber = PageIndex;
				var pageSize = PageSize;

				if (HasPreviousPage) pageNumber = PageIndex - 1;

				return new PagingParams { PageNumber = pageNumber, PageSize = pageSize };
			}
		}

		public PagingParams NextPagingParams
		{
			get
			{
				var pageNumber = PageIndex;
				var pageSize = PageSize;

				if (HasNextPage) pageNumber = PageIndex + 1;

				return new PagingParams { PageNumber = pageNumber, PageSize = pageSize };
			}
		}

		public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
		{
			var count = await Task.FromResult(source.Count());
			var items = await Task.FromResult(source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList());

			return new PaginatedList<T>(items, count, pageIndex, pageSize);
		}
	}
}
