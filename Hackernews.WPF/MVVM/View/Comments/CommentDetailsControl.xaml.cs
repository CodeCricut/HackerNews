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
		public CommentViewModel CommentViewModel
		{
			get { return (CommentViewModel)GetValue(CommentViewModelProperty); }
			set { SetValue(CommentViewModelProperty, value); }
		}

		public static readonly DependencyProperty CommentViewModelProperty =
			DependencyProperty.Register("CommentViewModel", typeof(CommentViewModel), typeof(CommentDetailsControl), new PropertyMetadata(default(CommentViewModel)));

		public CommentDetailsControl()
		{
			InitializeComponent();

			rootElement.DataContext = this;
		}
	}
}
