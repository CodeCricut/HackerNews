using Hackernews.WPF.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Hackernews.WPF.ViewModels
{
	public enum NavigationModelType
	{
		Boards,
		Articles,
		Comments
	}

	public class NavigationViewModel : BaseViewModel
	{
		public NavigationViewModel(BoardsListViewModel boardsListViewModel, ArticlesViewModel articlesViewModel, CommentListViewModel commentListViewModel)
		{
			NavigationModelType = NavigationModelType.Boards;

			SelectBoardsCommand = new AsyncDelegateCommand(SelectBoardsAsync);
			SelectArticlesCommand = new AsyncDelegateCommand(SelectArticlesAsync);
			SelectCommentsCommand = new AsyncDelegateCommand(SelectCommentsAsync);

			_boardsListViewModel = boardsListViewModel;
			_articlesViewModel = articlesViewModel;
			_commentListViewModel = commentListViewModel;
		}

		#region Public Properties
		public ICommand SelectBoardsCommand { get; }
		public ICommand SelectArticlesCommand { get; }
		public ICommand SelectCommentsCommand { get; }

		private NavigationModelType _navigationModelType;
		private readonly BoardsListViewModel _boardsListViewModel;
		private readonly ArticlesViewModel _articlesViewModel;
		private readonly CommentListViewModel _commentListViewModel;

		public NavigationModelType NavigationModelType
		{
			get => _navigationModelType;
			set
			{
				if (_navigationModelType != value)
				{
					_navigationModelType = value;
					RaisePropertyChanged();
					RaisePropertyChanged(nameof(AreBoardsSelected));
					RaisePropertyChanged(nameof(AreArticlesSelected));
					RaisePropertyChanged(nameof(AreCommentsSelected));
				}
			}
		}

		public bool AreBoardsSelected
		{
			get =>
				NavigationModelType != NavigationModelType.Articles &&
				NavigationModelType != NavigationModelType.Comments;
		}
		public bool AreArticlesSelected { get => NavigationModelType == NavigationModelType.Articles; }
		public bool AreCommentsSelected { get => NavigationModelType == NavigationModelType.Comments; }
		#endregion

		public async Task SelectBoardsAsync()
		{
			NavigationModelType = NavigationModelType.Boards;
			await _boardsListViewModel.LoadBoardsAsync();
		}

		public async Task SelectArticlesAsync()
		{
			NavigationModelType = NavigationModelType.Articles;
			await _articlesViewModel.LoadArticlesAsync();
		}

		public async Task SelectCommentsAsync()
		{
			NavigationModelType = NavigationModelType.Comments;
			await _commentListViewModel.LoadCommentsAsync();
		}
	}
}
