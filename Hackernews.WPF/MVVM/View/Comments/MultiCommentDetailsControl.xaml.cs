using Hackernews.WPF.MVVM.ViewModel;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Hackernews.WPF.MVVM.View.Comments
{
	/// <summary>
	/// Interaction logic for MultiCommentDetailsControl.xaml
	/// </summary>
	public partial class MultiCommentDetailsControl : UserControl
	{
		public ObservableCollection<CommentViewModel> CommentViewModels
		{
			get { return (ObservableCollection<CommentViewModel>)GetValue(CommentViewModelsProperty); }
			set { SetValue(CommentViewModelsProperty, value); }
		}

		public static readonly DependencyProperty CommentViewModelsProperty =
			DependencyProperty.Register("CommentViewModels", typeof(ObservableCollection<CommentViewModel>), typeof(MultiCommentDetailsControl), new PropertyMetadata(default(ObservableCollection<CommentViewModel>)));


		public MultiCommentDetailsControl()
		{
			InitializeComponent();

			rootElement.DataContext = this;
		}
	}
}
