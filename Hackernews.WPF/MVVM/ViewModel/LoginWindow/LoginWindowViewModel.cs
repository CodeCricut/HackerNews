using Hackernews.WPF.Helpers;
using Hackernews.WPF.Messages.Application;
using Hackernews.WPF.MVVM.ViewModel;
using Hackernews.WPF.Services;
using HackerNews.WPF.MessageBus.Core;
using HackerNews.WPF.MessageBus.ViewModel.LoginWindow;
using System.Windows.Input;

namespace Hackernews.WPF.ViewModels
{
	public class LoginWindowViewModel : BaseViewModel
	{
		private readonly IEventAggregator _ea;
		private readonly IViewManager _viewManager;
		private bool _loading;
		public bool Loading
		{
			get { return _loading; }
			set { _loading = value; RaisePropertyChanged(); RaisePropertyChanged(nameof(NotLoading)); }
		}
		public bool NotLoading { get => !Loading; }

		private bool _invalidCreds;
		public bool InvalidUserInput
		{
			get { return _invalidCreds; }
			set { _invalidCreds = value; RaisePropertyChanged(); RaisePropertyChanged(nameof(ValidCreds)); }
		}
		public bool ValidCreds { get => !InvalidUserInput; }

		public ICommand CloseCommand { get; }

		#region View switcher
		private object _selectedViewModel;
		public object SelectedViewModel
		{
			get => _selectedViewModel; set
			{
				_selectedViewModel = value;
				RaisePropertyChanged();
				RaisePropertyChanged(nameof(LoginModelSelected));
				RaisePropertyChanged(nameof(RegisterModelSelected));
			}
		}

		public bool LoginModelSelected => SelectedViewModel == LoginModelViewModel;
		public bool RegisterModelSelected => SelectedViewModel == RegisterViewModel;

		public ICommand SelectLoginModelCommand { get; }
		public ICommand SelectRegisterModelCommand { get; }

		private void SelectLoginModel(object parameter = null) => SelectedViewModel = LoginModelViewModel;
		private void SelectRegisterModel(object parameter = null) => SelectedViewModel = RegisterViewModel;
		#endregion

		public LoginModelViewModel LoginModelViewModel { get; }

		public RegisterViewModel RegisterViewModel { get; }

		public LoginWindowViewModel(IEventAggregator ea,
			LoginModelViewModel loginModelVM,
			RegisterViewModel registerVM,
			IViewManager viewManager)
		{
			_ea = ea;

			LoginModelViewModel = loginModelVM;
			RegisterViewModel = registerVM;
			_viewManager = viewManager;
			SelectLoginModelCommand = new DelegateCommand(SelectLoginModel);
			SelectRegisterModelCommand = new DelegateCommand(SelectRegisterModel);

			SelectedViewModel = LoginModelViewModel;

			CloseCommand = new DelegateCommand(_ => CloseWindow());

			ea.RegisterHandler<LoginWindowLoadingChangedMessage>(LoadingChanged);
			ea.RegisterHandler<LoginWindowInvalidUserInputChanged>(InvalidUserInputChanged);

			ea.RegisterHandler<CloseLoginWindowMessage>(msg => CloseWindow());
		}

		private void LoadingChanged(LoginWindowLoadingChangedMessage msg) => Loading = msg.IsLoading;

		private void InvalidUserInputChanged(LoginWindowInvalidUserInputChanged msg) => InvalidUserInput = msg.InvalidUserInput;

		public void ShowWindow()
		{
			_viewManager.Show(this);
		}

		public void CloseWindow()
		{
			_ea.UnregisterHandler<CloseLoginWindowMessage>(msg => CloseWindow());
			_viewManager.Close(this);
		}
	}
}
