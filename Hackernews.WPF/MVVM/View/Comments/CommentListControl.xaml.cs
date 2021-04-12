using Hackernews.WPF.ViewModels;
using System.Windows;
using System.Windows.Controls;

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
