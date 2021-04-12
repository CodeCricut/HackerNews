using Hackernews.WPF.ViewModels;
using HackerNews.Domain.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hackernews.WPF.MVVM.Model
{
	public class PaginatedListViewModel<T> : BaseViewModel
	{
		private PaginatedList<T> _page;
		public PaginatedList<T> Page 
		{ 
			get => _page; 
			set => Set(ref _page, value, ""); 
		}

		public IEnumerable<T> Items { get => Page?.Items; }

		public int CurrentPageNumber { get => Page?.PageIndex ?? 0; }
		public int TotalPages { get => Page?.TotalPages ?? 0; }

		public int TotalCount { get => Page?.TotalCount ?? 0; }

		public bool HasPrevPage { get => Page?.HasPreviousPage ?? false; }
		public PagingParams PrevPagingParams { get => Page?.PreviousPagingParams; }

		public bool HasNextPage { get => Page?.HasNextPage ?? false; }
		public PagingParams NextPagingParams { get => Page?.NextPagingParams; }
	}
}
