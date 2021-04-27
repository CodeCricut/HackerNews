using Hackernews.WPF.MVVM.ViewModel;
using Hackernews.WPF.Services;
using Hackernews.WPF.ViewModels;
using HackerNews.WPF.MessageBus.Core;

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
		private readonly IBoardCreationViewModelFactory _boardCreationViewModelFactory;

		public MainWindowVmFactory(IEventAggregator ea,
			IViewManager viewManager,
			PrivateUserViewModel userVm,
			MainWindowEntityViewModel entityVm,
			MainWindowFullscreenViewModel fullscreenVm,
			IBoardCreationViewModelFactory boardCreationViewModelFactory)
		{
			_ea = ea;
			_viewManager = viewManager;
			_userVm = userVm;
			_entityVm = entityVm;
			_fullscreenVm = fullscreenVm;
			_boardCreationViewModelFactory = boardCreationViewModelFactory;
		}

		public MainWindowViewModel Create() => new MainWindowViewModel(
			_ea,
			_viewManager,
			_userVm,
			_entityVm,
			_fullscreenVm,
			_boardCreationViewModelFactory.Create());
	}
}
