using Hackernews.WPF.MVVM.ViewModel;
using Hackernews.WPF.Services;
using Hackernews.WPF.ViewModels;
using HackerNews.WPF.MessageBus.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hackernews.WPF.Factories.ViewModels
{
	public class MainWindowViewModelFactory : ViewModelFactory<MainWindowViewModel>
	{
		private readonly IEventAggregator _ea;
		private readonly IViewManager _viewManager;
		private readonly PrivateUserViewModel _userVM;
		private readonly MainWindowEntityViewModel _entityVm;
		private readonly MainWindowFullscreenViewModel _fullscreenVm;
		private readonly EntityCreationViewModel _entityCreationVm;

		public MainWindowViewModelFactory(IEventAggregator ea,
			IViewManager viewManager,
			PrivateUserViewModel userVM,
			MainWindowEntityViewModel entityVm,
			MainWindowFullscreenViewModel fullscreenVm,
			EntityCreationViewModel entityCreationVm)
		{
			_ea = ea;
			_viewManager = viewManager;
			_userVM = userVM;
			_entityVm = entityVm;
			_fullscreenVm = fullscreenVm;
			_entityCreationVm = entityCreationVm;
		}

		public MainWindowViewModel Create() => new MainWindowViewModel(_ea, _viewManager, _userVM, _entityVm, _fullscreenVm, _entityCreationVm);
	}
}
