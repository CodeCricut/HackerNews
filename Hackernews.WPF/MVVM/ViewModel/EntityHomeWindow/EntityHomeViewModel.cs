using Hackernews.WPF.Helpers;
using Hackernews.WPF.MVVM.ViewModel.Articles;
using Hackernews.WPF.MVVM.ViewModel.Boards;
using Hackernews.WPF.Services;
using Hackernews.WPF.ViewModels;
using System.Windows.Input;

namespace Hackernews.WPF.MVVM.ViewModel
{
	public class EntityHomeViewModel : BaseViewModel
	{
		private readonly IViewManager _viewManager;

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
			IViewManager viewManager)
		{
			_viewManager = viewManager;

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
