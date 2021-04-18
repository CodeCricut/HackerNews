using Hackernews.WPF.MVVM.ViewModel.Boards;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
