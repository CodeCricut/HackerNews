using Hackernews.WPF.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace Hackernews.WPF.Controls
{
	/// <summary>
	/// Interaction logic for ArticleListControl.xaml
	/// </summary>
	public partial class ArticleListControl : UserControl
	{
		public ArticleListViewModel ArticleListViewModel
		{
			get { return (ArticleListViewModel)GetValue(ArticleListViewModelProperty); }
			set { SetValue(ArticleListViewModelProperty, value); }
		}

		public static readonly DependencyProperty ArticleListViewModelProperty =
			DependencyProperty.Register("ArticleListViewModel", typeof(ArticleListViewModel), typeof(ArticleListControl));



		public ArticleListControl()
		{
			InitializeComponent();

			rootElement.DataContext = this;
		}
	}
}
