using Hackernews.WPF.ViewModels;
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

namespace Hackernews.WPF.Controls
{
	/// <summary>
	/// Interaction logic for BoardDetailsControl.xaml
	/// </summary>
	public partial class BoardDetailsControl : UserControl
	{
		public BoardsListViewModel BoardsListViewModel
		{
			get { return (BoardsListViewModel)GetValue(BoardsListViewModelProperty); }
			set { SetValue(BoardsListViewModelProperty, value); }
		}

		// Using a DependencyProperty as the backing store for BoardsListViewModel.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty BoardsListViewModelProperty =
			DependencyProperty.Register("BoardsListViewModel", typeof(BoardsListViewModel), typeof(BoardDetailsControl), new PropertyMetadata(default(BoardsListViewModel)));


		public BoardDetailsControl()
		{
			InitializeComponent();

			rootElement.DataContext = this;
		}
	}
}
