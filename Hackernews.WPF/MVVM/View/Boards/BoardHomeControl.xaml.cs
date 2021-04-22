using Hackernews.WPF.MVVM.ViewModel.Boards;
using System.Windows;
using System.Windows.Controls;

namespace Hackernews.WPF.MVVM.View.Boards
{
	/// <summary>
	/// Interaction logic for BoardHomeControl.xaml
	/// </summary>
	public partial class BoardHomeControl : UserControl
	{
		public BoardHomeViewModel BoardHomeViewModel
		{
			get { return (BoardHomeViewModel)GetValue(BoardHomeViewModelProperty); }
			set { SetValue(BoardHomeViewModelProperty, value); }
		}
		public static readonly DependencyProperty BoardHomeViewModelProperty =
			DependencyProperty.Register("BoardHomeViewModel", typeof(BoardHomeViewModel), typeof(BoardHomeControl), new PropertyMetadata(default(BoardHomeViewModel)));

		public BoardHomeControl()
		{
			InitializeComponent();

			rootElement.DataContext = this;
		}

		private void UserControl_Loaded(object sender, RoutedEventArgs e)
		{
			BoardHomeViewModel.LoadBoardCommand.Execute();
		}
	}
}
