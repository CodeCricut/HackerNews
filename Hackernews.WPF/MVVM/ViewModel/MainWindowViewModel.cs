using Hackernews.WPF.ApiClients;
using Hackernews.WPF.Core.Commands;
using Hackernews.WPF.Helpers;
using Hackernews.WPF.MVVM.ViewModel;
using Hackernews.WPF.MVVM.ViewModel.Comments;
using Hackernews.WPF.MVVM.ViewModel.Common;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Hackernews.WPF.ViewModels
{
	public class MainWindowViewModel : BaseViewModel
	{
		public Action? CloseAction { get; set; }
		public ICommand CloseCommand { get; }

		public Action? LogoutAction { get; set; }
		public ICommand LogoutCommand { get; }

		public PrivateUserViewModel PrivateUserViewModel { get; }

		#region Select view commands
		public ICommand SelectHomeCommand { get; }
		public ICommand SelectProfileCommand { get; }
		public ICommand SelectSettingsCommand { get; }

		public AsyncDelegateCommand SelectUsersCommand { get; }
		public ICommand SelectBoardsCommand { get; }
		public ICommand SelectArticlesCommand { get; }
		public ICommand SelectCommentsCommand { get; }
		#endregion

		#region Fullscreen VMs
		public bool NotInFullscreenMode { get => SelectedFullscreenViewModel == null; }

		private object _selectedFullscreenViewModel;
		public object SelectedFullscreenViewModel
		{
			get => _selectedFullscreenViewModel;
			set
			{
				_selectedFullscreenViewModel = value;
				RaisePropertyChanged();
				RaisePropertyChanged(nameof(NotInFullscreenMode));
			}
		}

		public HomeViewModel HomeViewModel { get; }
		public ProfileViewModel ProfileViewModel { get; }
		public SettingsViewModel SettingsViewModel { get; }
		#endregion

		#region List VMs
		private IPageNavigatorViewModel _selectedListViewModel;
		public IPageNavigatorViewModel SelectedListViewModel
		{
			get { return _selectedListViewModel; }
			set
			{
				_selectedListViewModel = value;
				RaisePropertyChanged();
			}
		}

		public BoardsListViewModel BoardListViewModel { get; }
		public ArticleListViewModel ArticleListViewModel { get; }
		public CommentListViewModel CommentListViewModel { get; }
		public UserListViewModel UserListViewModel { get; }
		#endregion

		#region Details VMs
		private object _selectedDetailsViewModel;
		public object SelectedDetailsViewModel
		{
			get { return _selectedDetailsViewModel; }
			set
			{
				_selectedDetailsViewModel = value;
				RaisePropertyChanged();
			}
		}

		public BoardViewModel BoardViewModel { get; }
		public ArticleViewModel ArticleViewModel { get; }
		public CommentViewModel CommentViewModel { get; }
		public PublicUserViewModel PublicUserViewModel { get; }
		#endregion

		public MainWindowViewModel(IApiClient apiClient, PrivateUserViewModel userVM)
		{
			CloseCommand = new DelegateCommand(_ => CloseAction?.Invoke());
			LogoutCommand = new DelegateCommand(_ => LogoutAction?.Invoke());

			PrivateUserViewModel = new PrivateUserViewModel(apiClient);

			UserListViewModel = new UserListViewModel(apiClient);
			BoardListViewModel = new BoardsListViewModel(apiClient);
			ArticleListViewModel = new ArticleListViewModel(vm => new LoadArticlesCommand(vm, apiClient, userVM));
			CommentListViewModel = new CommentListViewModel(vm => new LoadCommentsCommand(vm, apiClient));

			HomeViewModel = new HomeViewModel();
			ProfileViewModel = new ProfileViewModel(PrivateUserViewModel)
			{
				LogoutAction = () => this.LogoutAction?.Invoke()
			};
			SettingsViewModel = new SettingsViewModel();

			PublicUserViewModel = new PublicUserViewModel();
			BoardViewModel = new BoardViewModel(apiClient);
			ArticleViewModel = new ArticleViewModel(userVM, apiClient);
			CommentViewModel = new CommentViewModel();

			SelectHomeCommand = new DelegateCommand(SelectHome);
			SelectProfileCommand = new DelegateCommand(SelectProfile);
			SelectSettingsCommand = new DelegateCommand(SelectSettings);

			SelectUsersCommand = new AsyncDelegateCommand(SelectUsersAsync);
			SelectBoardsCommand = new AsyncDelegateCommand(SelectBoardsAsync);
			SelectArticlesCommand = new AsyncDelegateCommand(SelectArticlesAsync);
			SelectCommentsCommand = new AsyncDelegateCommand(SelectCommentsAsync);
		}

		public void SelectHome(object parameter = null)
		{
			SelectedDetailsViewModel = null;
			SelectedListViewModel = null;
			SelectedFullscreenViewModel = HomeViewModel;
		}

		public void SelectProfile(object parameter = null)
		{

			SelectedDetailsViewModel = null;
			SelectedListViewModel = null;
			SelectedFullscreenViewModel = ProfileViewModel;

			PrivateUserViewModel.TryLoadUserCommand.Execute(null);
		}

		public void SelectSettings(object parameter = null)
		{

			SelectedDetailsViewModel = null;
			SelectedListViewModel = null;
			SelectedFullscreenViewModel = SettingsViewModel;
		}

		public async Task SelectUsersAsync(object parameter = null)
		{
			SelectedListViewModel = UserListViewModel;
			SelectedDetailsViewModel = PublicUserViewModel;
			SelectedFullscreenViewModel = null;
			await Task.Factory.StartNew(() => UserListViewModel.LoadCommand.TryExecute());
		}

		public async Task SelectBoardsAsync(object parameter = null)
		{
			SelectedListViewModel = BoardListViewModel;
			SelectedDetailsViewModel = BoardViewModel;
			SelectedFullscreenViewModel = null;
			await Task.Factory.StartNew(() => BoardListViewModel.LoadCommand.TryExecute());
		}

		public async Task SelectArticlesAsync(object parameter = null)
		{
			SelectedListViewModel = ArticleListViewModel;
			SelectedDetailsViewModel = ArticleViewModel;
			SelectedFullscreenViewModel = null;
			await Task.Factory.StartNew(() => ArticleListViewModel.LoadCommand.TryExecute());
		}

		public async Task SelectCommentsAsync(object parameter = null)
		{
			SelectedListViewModel = CommentListViewModel;
			SelectedDetailsViewModel = CommentViewModel;
			SelectedFullscreenViewModel = null;
			await Task.Factory.StartNew(() => CommentListViewModel.LoadCommand.TryExecute());
		}
	}
}
