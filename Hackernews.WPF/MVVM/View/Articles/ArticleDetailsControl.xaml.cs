using Hackernews.WPF.MVVM.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace Hackernews.WPF.Controls
{
	/// <summary>
	/// Interaction logic for ArticleDetailsControl.xaml
	/// </summary>
	public partial class ArticleDetailsControl : UserControl
	{


		public ArticleViewModel ArticleViewModel
		{
			get { return (ArticleViewModel)GetValue(ArticleViewModelProperty); }
			set { SetValue(ArticleViewModelProperty, value); }
		}

		public static readonly DependencyProperty ArticleViewModelProperty =
			DependencyProperty.Register("ArticleViewModel", typeof(ArticleViewModel), typeof(ArticleDetailsControl), new PropertyMetadata(default(ArticleViewModel)));



		//public ArticleListViewModel ArticleListViewModel
		//{
		//	get { return (ArticleListViewModel)GetValue(ArticleListViewModelProperty); }
		//	set { SetValue(ArticleListViewModelProperty, value); }
		//}

		//public static readonly DependencyProperty ArticleListViewModelProperty =
		//	DependencyProperty.Register(nameof(ArticleListViewModel), typeof(ArticleListViewModel), typeof(ArticleDetailsControl), new PropertyMetadata(default(ArticleListViewModel)));

		public ArticleDetailsControl()
		{
			InitializeComponent();

			rootElement.DataContext = this;
		}
	}
}
