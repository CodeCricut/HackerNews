using Hackernews.WPF.MVVM.ViewModel;
using Hackernews.WPF.MVVM.ViewModel.Common;
using HackerNews.Domain.Common.Models.Comments;
using System.Windows;
using System.Windows.Controls;

namespace Hackernews.WPF.Controls
{
	public partial class CommentListControl : UserControl
	{
		public EntityListViewModel<CommentViewModel, GetCommentModel> CommentListViewModel
		{
			get { return (EntityListViewModel<CommentViewModel, GetCommentModel>)GetValue(CommentListViewModelProperty); }
			set { SetValue(CommentListViewModelProperty, value); }
		}

		public static readonly DependencyProperty CommentListViewModelProperty =
			DependencyProperty.Register("CommentListViewModel", typeof(EntityListViewModel<CommentViewModel, GetCommentModel>), typeof(CommentListControl));

		public CommentListControl()
		{
			InitializeComponent();

			rootElement.DataContext = this;
		}
	}
}
