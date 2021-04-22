using Hackernews.WPF.ApiClients;
using Hackernews.WPF.Core;
using Hackernews.WPF.Helpers;
using Hackernews.WPF.MVVM.Model;
using Hackernews.WPF.MVVM.ViewModel.Boards;
using Hackernews.WPF.MVVM.ViewModel.Common;
using Hackernews.WPF.ViewModels;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Boards;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Hackernews.WPF.MVVM.ViewModel
{
	public class BoardsListViewModel : BaseViewModel
	{
		//public PagingParams PagingParams = new PagingParams();

		public BoardViewModel BoardViewModel { get; } 

		public ObservableCollection<BoardViewModel> Boards { get; private set; } = new ObservableCollection<BoardViewModel>();

		public PaginatedListViewModel<GetBoardModel> BoardPageVM { get; set; }

		public BaseCommand LoadCommand { get; set; }
		//public AsyncDelegateCommand NextPageCommand { get; }
		//public AsyncDelegateCommand PrevPageCommand { get; }

		//public int CurrentPage
		//{
		//	get => BoardPageVM.CurrentPage;
		//}
		//public int TotalPages
		//{
		//	get => BoardPageVM.TotalPages;
		//}

		public BoardsListViewModel(IApiClient apiClient, PrivateUserViewModel userVM) : this(apiClient, thisVM => new LoadBoardsCommand(thisVM, apiClient, userVM), userVM)
		{
		}

		public BoardsListViewModel(IApiClient apiClient, CreateBaseCommand<BoardsListViewModel> createLoadCommand, PrivateUserViewModel userVM)
		{
			LoadCommand = createLoadCommand(this);

			BoardPageVM = new PaginatedListViewModel<GetBoardModel>(LoadCommand);

			// TODO: check to see if this property is needed still.
			BoardViewModel = new BoardViewModel(apiClient, userVM);

			//NextPageCommand = new AsyncDelegateCommand(NextPageAsync, _ => BoardPageVM.HasNextPage);
			//PrevPageCommand = new AsyncDelegateCommand(PrevPageAsync, _ => BoardPageVM.HasPrevPage);

			BoardPageVM.PropertyChanged += new PropertyChangedEventHandler((obj, target) => RaisePageChanged());
		}

		public void RaisePageChanged()
		{
			BoardPageVM.RaisePropertyChanged(nameof(BoardPageVM.CurrentPage));
			BoardPageVM.RaisePropertyChanged(nameof(BoardPageVM.TotalPages));
		}

		//private async Task NextPageAsync(object parameter = null)
		//{
		//	PagingParams = BoardPageVM.NextPagingParams;
		//	await Task.Factory.StartNew(() => LoadCommand.TryExecute(parameter));
		//}

		//private async Task PrevPageAsync(object parameter = null)
		//{
		//	PagingParams = BoardPageVM.PrevPagingParams;
		//	await Task.Factory.StartNew(() => LoadCommand.TryExecute(parameter));
		//}
	}
}
