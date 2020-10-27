﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Application.Common.Models
{
	public class PaginatedList<T>
	{
		public List<T> Items { get; }
		public int PageIndex { get; }
		public int TotalPages { get; }
		public int TotalCount { get; }
		public int PageSize { get; set; }

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

		public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
		{
			var count = await Task.FromResult(source.Count());
			var items = await Task.FromResult(source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList());

			return new PaginatedList<T>(items, count, pageIndex, pageSize);
		}
	}
}
