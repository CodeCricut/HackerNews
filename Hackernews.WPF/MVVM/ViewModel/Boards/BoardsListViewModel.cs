using Hackernews.WPF.ApiClients;
using Hackernews.WPF.Helpers;
using Hackernews.WPF.MVVM.Model;
using Hackernews.WPF.MVVM.ViewModel.Boards;
using Hackernews.WPF.MVVM.ViewModel.Common;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Boards;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Hackernews.WPF.ViewModels
{
	public class BoardsListViewModel : BaseViewModel, IPageNavigatorViewModel
	{
		public PagingParams PagingParams = new PagingParams();

		public BoardViewModel BoardViewModel { get; } = new BoardViewModel();

		public ObservableCollection<GetBoardModel> Boards { get; private set; } = new ObservableCollection<GetBoardModel>();

		public PaginatedListViewModel<GetBoardModel> BoardPageVM { get; set; }

		public LoadBoardsCommand LoadCommand { get; }
		public AsyncDelegateCommand NextPageCommand { get; }
		public AsyncDelegateCommand PrevPageCommand { get; }

		public int CurrentPage
		{
			get => BoardPageVM.CurrentPageNumber;
		}
		public int TotalPages
		{
			get => BoardPageVM.TotalPages;
		}

		public BoardsListViewModel(IApiClient apiClient)
		{
			BoardPageVM = new PaginatedListViewModel<GetBoardModel>();

			LoadCommand = new LoadBoardsCommand(this, apiClient);
			NextPageCommand = new AsyncDelegateCommand(NextPageAsync, () => BoardPageVM.HasNextPage);
			PrevPageCommand = new AsyncDelegateCommand(PrevPageAsync, () => BoardPageVM.HasPrevPage);

			BoardPageVM.PropertyChanged += new PropertyChangedEventHandler((obj, target) => RaisePageChanged());
		}

		public void RaisePageChanged()
		{
			RaisePropertyChanged(nameof(CurrentPage));
			RaisePropertyChanged(nameof(TotalPages));
		}

		private async Task NextPageAsync()
		{
			PagingParams = BoardPageVM.NextPagingParams;
			await Task.Factory.StartNew(() => LoadCommand.TryExecute());

		}

		private async Task PrevPageAsync()
		{
			PagingParams = BoardPageVM.PrevPagingParams;
			await Task.Factory.StartNew(() => LoadCommand.TryExecute());
		}
	}
}
