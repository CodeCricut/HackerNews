using Hackernews.WPF.ApiClients;
using Hackernews.WPF.Helpers;
using Hackernews.WPF.Messages.Application;
using Hackernews.WPF.MVVM.ViewModel;
using Hackernews.WPF.Services;
using HackerNews.WPF.MessageBus.Core;
using HackerNews.WPF.MessageBus.ViewModel.MainWindow;
using HackerNews.WPF.MessageBus.ViewModel.MainWindow.Profile;
using System;
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
		public EntityCreationViewModel EntityCreationViewModel { get; }
		#endregion

		public ICommand CloseCommand { get; }

		public MainWindowViewModel(IEventAggregator ea,
			IViewManager viewManager,
			PrivateUserViewModel userVM,
			MainWindowEntityViewModel entityVm,
			MainWindowFullscreenViewModel fullscreenVm,
			EntityCreationViewModel entityCreationVm)
		{
			_ea = ea;
			_viewManager = viewManager;

			PrivateUserViewModel = userVM;
			EntityVM = entityVm;
			FullscreenVM = fullscreenVm;
			EntityCreationViewModel = entityCreationVm;

			CloseCommand = new DelegateCommand(_ => CloseWindow());

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
	}
}
