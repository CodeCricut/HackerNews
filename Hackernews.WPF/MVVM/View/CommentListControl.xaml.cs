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
	public partial class CommentListControl : UserControl
	{
		public CommentListViewModel CommentListViewModel
		{
			get { return (CommentListViewModel)GetValue(CommentListViewModelProperty); }
			set { SetValue(CommentListViewModelProperty, value); }
		}

		public static readonly DependencyProperty CommentListViewModelProperty =
			DependencyProperty.Register("CommentListViewModel", typeof(CommentListViewModel), typeof(CommentListControl), new PropertyMetadata(default(CommentListViewModel)));


		public CommentListControl()
		{
			InitializeComponent();

			rootElement.DataContext = this;
		}
	}
}
