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
	/// Interaction logic for CommentDetailsControl.xaml
	/// </summary>
	public partial class CommentDetailsControl : UserControl
	{
		public CommentListViewModel CommentListViewModel
		{
			get { return (CommentListViewModel)GetValue(CommentListViewModelProperty); }
			set { SetValue(CommentListViewModelProperty, value); }
		}

		public static readonly DependencyProperty CommentListViewModelProperty =
			DependencyProperty.Register("CommentListViewModel", typeof(CommentListViewModel), typeof(CommentDetailsControl), new PropertyMetadata(default(CommentListViewModel)));

		public CommentDetailsControl()
		{
			InitializeComponent();

			rootElement.DataContext = this;
		}
	}
}
