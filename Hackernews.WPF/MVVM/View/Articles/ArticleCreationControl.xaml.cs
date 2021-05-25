using Hackernews.WPF.MVVM.ViewModel.Articles;
using System.Windows;
using System.Windows.Controls;

namespace Hackernews.WPF.MVVM.View.Articles
{
	/// <summary>
	/// Interaction logic for ArticleCreationControl.xaml
	/// </summary>
	public partial class ArticleCreationControl : UserControl
	{
		public ArticleCreationViewModel ArticleCreationViewModel
		{
			get { return (ArticleCreationViewModel)GetValue(ArticleCreationViewModelProperty); }
			set { SetValue(ArticleCreationViewModelProperty, value); }
		}

		public static readonly DependencyProperty ArticleCreationViewModelProperty =
			DependencyProperty.Register("ArticleCreationViewModel", typeof(ArticleCreationViewModel), typeof(ArticleCreationControl), new PropertyMetadata(default(ArticleCreationViewModel)));

		public ArticleCreationControl()
		{
			InitializeComponent();

			rootElement.DataContext = this;
		}
	}
}
