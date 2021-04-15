using Hackernews.WPF.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
