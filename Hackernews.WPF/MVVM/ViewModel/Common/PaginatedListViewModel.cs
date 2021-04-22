using Hackernews.WPF.Core;
using Hackernews.WPF.Helpers;
using Hackernews.WPF.MVVM.ViewModel.Common;
using Hackernews.WPF.ViewModels;
using HackerNews.Domain.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hackernews.WPF.MVVM.Model
{
	/// <summary>
	/// Represents a page of entities and the logic related to loading this page of entities.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class PaginatedListViewModel<T> : BaseViewModel, IPageNavigatorViewModel
	{
		public PagingParams PagingParams { get; private set; } = new PagingParams();

		private PaginatedList<T> _page;
		public PaginatedList<T> Page
		{
			get => _page;
			set { Set(ref _page, value, ""); RaisePropertyChanged(""); }
		}

		public IEnumerable<T> Items { get => Page?.Items; }

		public int CurrentPage { get => Page?.PageIndex ?? 0; }
		public int TotalPages { get => Page?.TotalPages ?? 0; }

		public int TotalCount { get => Page?.TotalCount ?? 0; }

		public bool HasPrevPage { get => Page?.HasPreviousPage ?? false; }
		public PagingParams PrevPagingParams { get => Page?.PreviousPagingParams; }

		public bool HasNextPage { get => Page?.HasNextPage ?? false; }
		public PagingParams NextPagingParams { get => Page?.NextPagingParams; }

		public BaseCommand LoadCommand { get; }
		public AsyncDelegateCommand PrevPageCommand { get; }
		public AsyncDelegateCommand NextPageCommand { get; }

		public PaginatedListViewModel(BaseCommand loadCommand)
		{
			LoadCommand = loadCommand;
			PrevPageCommand = new AsyncDelegateCommand(TryLoadPrevPage, _ => HasPrevPage);
			NextPageCommand = new AsyncDelegateCommand(TryLoadNextPage, _ => HasNextPage);
		}

		/// <summary>
		/// If there is a next page, load it.
		/// </summary>
		public async Task TryLoadNextPage(object parameter = null)
		{
			if (!HasNextPage) return;

			App.Current.Dispatcher.Invoke(() =>
			{
				PagingParams = NextPagingParams;
				LoadCommand.TryExecute(parameter);

				PrevPageCommand.RaiseCanExecuteChanged();
				NextPageCommand.RaiseCanExecuteChanged();
			});
		}

		/// <summary>
		/// If there is a previous page, load it.
		/// </summary>
		public async Task TryLoadPrevPage(object parameter = null)
		{
			if (!HasPrevPage) return;

			await App.Current.Dispatcher.Invoke(async () =>
			{
				PagingParams = PrevPagingParams;
				LoadCommand.TryExecute(parameter);

				PrevPageCommand.RaiseCanExecuteChanged();
				NextPageCommand.RaiseCanExecuteChanged();
			});
		}
	}
}
