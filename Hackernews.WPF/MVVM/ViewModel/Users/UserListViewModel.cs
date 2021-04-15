using Hackernews.WPF.ApiClients;
using Hackernews.WPF.Helpers;
using Hackernews.WPF.MVVM.Model;
using Hackernews.WPF.MVVM.ViewModel.Common;
using Hackernews.WPF.ViewModels;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Users;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Hackernews.WPF.MVVM.ViewModel
{
	public class UserListViewModel : BaseViewModel, IPageNavigatorViewModel
	{
		public PagingParams PagingParams = new PagingParams();

		public PublicUserViewModel UserViewModel { get; } = new PublicUserViewModel();

		public ObservableCollection<PublicUserViewModel> Users { get; private set; } = new ObservableCollection<PublicUserViewModel>();

		public PaginatedListViewModel<GetPublicUserModel> UserPageVM { get; set; } = new PaginatedListViewModel<GetPublicUserModel>();

		public LoadUsersCommand LoadCommand { get; }
		public AsyncDelegateCommand NextPageCommand { get; }
		public AsyncDelegateCommand PrevPageCommand { get; }

		public int CurrentPage
		{
			get => UserPageVM.CurrentPageNumber;
		}
		public int TotalPages
		{
			get => UserPageVM.TotalPages;
		}

		public UserListViewModel(IApiClient apiClient)
		{
			LoadCommand = new LoadUsersCommand(this, apiClient);
			NextPageCommand = new AsyncDelegateCommand(NextPageAsync, _ => UserPageVM.HasNextPage);
			PrevPageCommand = new AsyncDelegateCommand(PrevPageAsync, _ => UserPageVM.HasPrevPage);

			UserPageVM.PropertyChanged += new PropertyChangedEventHandler((obj, target) => RaisePageChanged());
		}

		public void RaisePageChanged()
		{
			RaisePropertyChanged(nameof(CurrentPage));
			RaisePropertyChanged(nameof(TotalPages));
		}

		private async Task NextPageAsync(object parameter = null)
		{
			PagingParams = UserPageVM.NextPagingParams;
			await Task.Factory.StartNew(() => LoadCommand.TryExecute(parameter));

		}

		private async Task PrevPageAsync(object parameter = null)
		{
			PagingParams = UserPageVM.PrevPagingParams;
			await Task.Factory.StartNew(() => LoadCommand.TryExecute(parameter));
		}
	}
}
