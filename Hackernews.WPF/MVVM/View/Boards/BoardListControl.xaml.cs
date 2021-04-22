using Hackernews.WPF.MVVM.ViewModel;
using Hackernews.WPF.MVVM.ViewModel.Common;
using HackerNews.Domain.Common.Models.Boards;
using System.Windows;
using System.Windows.Controls;

namespace Hackernews.WPF.Controls
{
	public partial class BoardListControl : UserControl
	{
		public EntityListViewModel<BoardViewModel, GetBoardModel> BoardsListViewModel
		{
			get { return (EntityListViewModel<BoardViewModel, GetBoardModel>)GetValue(BoardsListViewModelProperty); }
			set { SetValue(BoardsListViewModelProperty, value); }
		}

		public static readonly DependencyProperty BoardsListViewModelProperty =
			DependencyProperty.Register("BoardsListViewModel", typeof(EntityListViewModel<BoardViewModel, GetBoardModel>), typeof(BoardListControl));

		public BoardListControl()
		{
			InitializeComponent();

			rootElement.DataContext = this;
		}
	}
}
