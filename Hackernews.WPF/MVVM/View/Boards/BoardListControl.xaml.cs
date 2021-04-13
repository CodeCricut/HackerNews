using Hackernews.WPF.MVVM.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace Hackernews.WPF.Controls
{
	public partial class BoardListControl : UserControl
	{
		public BoardsListViewModel BoardsListViewModel
		{
			get { return (BoardsListViewModel)GetValue(BoardsListViewModelProperty); }
			set { SetValue(BoardsListViewModelProperty, value); }
		}

		public static readonly DependencyProperty BoardsListViewModelProperty =
			DependencyProperty.Register("BoardsListViewModel", typeof(BoardsListViewModel), typeof(BoardListControl), new PropertyMetadata(default(BoardsListViewModel)));

		public BoardListControl()
		{
			InitializeComponent();

			rootElement.DataContext = this;
		}
	}
}
