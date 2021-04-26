using Hackernews.WPF.Factories.ViewModels;
using Hackernews.WPF.Helpers;
using Hackernews.WPF.MVVM.ViewModel.Articles;
using Hackernews.WPF.MVVM.ViewModel.Boards;
using Hackernews.WPF.Services;
using Hackernews.WPF.ViewModels;
using HackerNews.ApiConsumer.Core;
using HackerNews.WPF.MessageBus.Core;
using System.Windows.Input;

namespace Hackernews.WPF.MVVM.ViewModel
{
	public class EntityHomeViewModel : BaseViewModel
	{
		private readonly IViewManager _viewManager;
		private readonly IArticleViewModelFactory _articleViewModelFactory;
		private readonly IArticleHomeViewModelFactory _articleHomeViewModelFactory;

		#region View switcher
		private object _selectedHomeViewModel;
		public object SelectedHomeViewModel
		{
			get => _selectedHomeViewModel;
			set
			{
				_selectedHomeViewModel = value;
				RaisePropertyChanged();
				RaisePropertyChanged(nameof(HomeVMIsSelected));
			}
		}
		public bool HomeVMIsSelected => SelectedHomeViewModel != null;
		#endregion

		public ICommand CloseCommand { get; }

		public EntityHomeViewModel(
			IViewManager viewManager,
			IArticleViewModelFactory articleViewModelFactory,
			IArticleHomeViewModelFactory articleHomeViewModelFactory)
		{
			_viewManager = viewManager;
			_articleViewModelFactory = articleViewModelFactory;
			_articleHomeViewModelFactory = articleHomeViewModelFactory;

			CloseCommand = new DelegateCommand(_ => _viewManager.Close(this));
		}

		public void ShowBoardHome(BoardHomeViewModel boardHomeVm)
		{
			SelectBoardVM(boardHomeVm);

			OpenWindow();
		}

		public void ShowArticleHome(ArticleHomeViewModel articleHomeVm)
		{
			SelectArticleVM(articleHomeVm);

			OpenWindow();
		}

		private void OpenWindow(object _ = null) => _viewManager.Show(this);

		private void SelectBoardVM(BoardHomeViewModel boardHomeVm)
		{
			SelectedHomeViewModel = boardHomeVm;
		}

		private void SelectArticleVM(ArticleHomeViewModel articleHomeVm)
		{
			

			SelectedHomeViewModel = articleHomeVm;
		}
	}
}
