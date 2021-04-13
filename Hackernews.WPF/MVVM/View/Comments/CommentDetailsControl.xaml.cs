using Hackernews.WPF.MVVM.ViewModel;
using System.Windows;
using System.Windows.Controls;

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
