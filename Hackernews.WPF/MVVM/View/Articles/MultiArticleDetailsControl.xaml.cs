using Hackernews.WPF.MVVM.ViewModel;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Hackernews.WPF.MVVM.View.Articles
{
	/// <summary>
	/// Interaction logic for MultiArticleDetailsControl.xaml
	/// </summary>
	public partial class MultiArticleDetailsControl : UserControl
	{
		public ObservableCollection<ArticleViewModel> ArticleViewModels
		{
			get { return (ObservableCollection<ArticleViewModel>)GetValue(ArticleViewModelsProperty); }
			set { SetValue(ArticleViewModelsProperty, value); }
		}

		public static readonly DependencyProperty ArticleViewModelsProperty =
			DependencyProperty.Register("ArticleViewModels", typeof(ObservableCollection<ArticleViewModel>), typeof(MultiArticleDetailsControl), new PropertyMetadata(default(ObservableCollection<ArticleViewModel>)));


		public MultiArticleDetailsControl()
		{
			InitializeComponent();

			rootElement.DataContext = this;
		}
	}
}
