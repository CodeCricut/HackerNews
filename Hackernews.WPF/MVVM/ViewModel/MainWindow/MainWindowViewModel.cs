using Hackernews.WPF.MVVM.ViewModel;
using Hackernews.WPF.MVVM.ViewModel.Boards;
using Hackernews.WPF.Services;
using HackerNews.WPF.Core.Commands;
using HackerNews.WPF.Core.ViewModel;
using HackerNews.WPF.MessageBus.Core;
using HackerNews.WPF.MessageBus.Messages.Application;
using System.Windows.Input;

namespace Hackernews.WPF.ViewModels
{
	public class MainWindowViewModel : BaseViewModel
	{
		private readonly IEventAggregator _ea;
		private readonly IViewManager _viewManager;

		#region Owned VMs
		public PrivateUserViewModel PrivateUserViewModel { get; }

		public MainWindowFullscreenViewModel FullscreenVM { get; }
		public MainWindowEntityViewModel EntityVM { get; }
		public BoardCreationViewModel BoardCreationViewModel { get; }
		#endregion

		public ICommand CloseCommand { get; }

		public MainWindowViewModel(IEventAggregator ea,
			IViewManager viewManager,
			PrivateUserViewModel userVM,
			MainWindowEntityViewModel entityVm,
			MainWindowFullscreenViewModel fullscreenVm,
			BoardCreationViewModel boardCreationViewModel)
		{
			_ea = ea;
			_viewManager = viewManager;

			PrivateUserViewModel = userVM;
			EntityVM = entityVm;
			FullscreenVM = fullscreenVm;
			BoardCreationViewModel = boardCreationViewModel;
			CloseCommand = new DelegateCommand(_ => CloseApplication());

			ea.RegisterHandler<CloseMainWindowMessage>(msg => CloseWindow());
		}

		public void OpenWindow()
		{
			_viewManager.Show(this);
		}

		public void CloseWindow()
		{
			_ea.UnregisterHandler<CloseMainWindowMessage>(msg => CloseWindow());
			_viewManager.Close(this);
		}

		private void CloseApplication()
		{
			_ea.SendMessage(new CloseApplicationMessage());
		}
	}
}
