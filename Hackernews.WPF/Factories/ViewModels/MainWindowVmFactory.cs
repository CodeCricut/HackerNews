using Hackernews.WPF.MVVM.ViewModel;
using Hackernews.WPF.Services;
using Hackernews.WPF.ViewModels;
using HackerNews.WPF.MessageBus.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hackernews.WPF.Factories.ViewModels
{
	public interface IMainWindowVmFactory
	{
		MainWindowViewModel Create();
	}

	public class MainWindowVmFactory : IMainWindowVmFactory
	{
		private readonly IEventAggregator _ea;
		private readonly IViewManager _viewManager;
		private readonly PrivateUserViewModel _userVm;
		private readonly MainWindowEntityViewModel _entityVm;
		private readonly MainWindowFullscreenViewModel _fullscreenVm;
		private readonly EntityCreationViewModel _entityCreationVm;

		public MainWindowVmFactory(IEventAggregator ea,
			IViewManager viewManager,
			PrivateUserViewModel userVm,
			MainWindowEntityViewModel entityVm,
			MainWindowFullscreenViewModel fullscreenVm,
			EntityCreationViewModel entityCreationVm)
		{
			_ea = ea;
			_viewManager = viewManager;
			_userVm = userVm;
			_entityVm = entityVm;
			_fullscreenVm = fullscreenVm;
			_entityCreationVm = entityCreationVm;
		}

		public MainWindowViewModel Create() => new MainWindowViewModel(
			_ea,
			_viewManager,
			_userVm,
			_entityVm,
			_fullscreenVm,
			_entityCreationVm);
	}
}
