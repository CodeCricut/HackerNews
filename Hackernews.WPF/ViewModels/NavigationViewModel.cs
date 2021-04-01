using Hackernews.WPF.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

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
		public NavigationViewModel()
		{
			NavigationModelType = NavigationModelType.Boards;

			SelectBoardsCommand = new DelegateCommand(SelectBoards);
			SelectArticlesCommand = new DelegateCommand(SelectArticles);
			SelectCommentsCommand = new DelegateCommand(SelectComments);
		}

		#region Public Properties
		public DelegateCommand SelectBoardsCommand { get; }
		public DelegateCommand SelectArticlesCommand { get; }
		public DelegateCommand SelectCommentsCommand { get; }

		private NavigationModelType _navigationModelType;
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

		public void SelectBoards()
		{
			NavigationModelType = NavigationModelType.Boards;
		}

		public void SelectArticles()
		{
			NavigationModelType = NavigationModelType.Articles;
		}

		public void SelectComments()
		{
			NavigationModelType = NavigationModelType.Comments;
		}
	}
}
