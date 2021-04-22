using Hackernews.WPF.MVVM.ViewModel;
using Hackernews.WPF.MVVM.ViewModel.Common;
using HackerNews.Domain.Common.Models.Articles;
using System.Windows;
using System.Windows.Controls;

namespace Hackernews.WPF.Controls
{
	/// <summary>
	/// Interaction logic for ArticleListControl.xaml
	/// </summary>
	public partial class ArticleListControl : UserControl
	{
		public EntityListViewModel<ArticleViewModel, GetArticleModel> ArticleListViewModel
		{
			get { return (EntityListViewModel<ArticleViewModel, GetArticleModel>)GetValue(ArticleListViewModelProperty); }
			set { SetValue(ArticleListViewModelProperty, value); }
		}

		// Using a DependencyProperty as the backing store for ArticleListViewModel.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty ArticleListViewModelProperty =
			DependencyProperty.Register("ArticleListViewModel", typeof(EntityListViewModel<ArticleViewModel, GetArticleModel>), typeof(ArticleListControl));

		public ArticleListControl()
		{
			InitializeComponent();

			rootElement.DataContext = this;
		}
	}
}
