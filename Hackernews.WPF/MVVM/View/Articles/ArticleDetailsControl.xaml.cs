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

		public bool EnableDetailsButton
		{
			get { return (bool)GetValue(EnableDetailsButtonProperty); }
			set { SetValue(EnableDetailsButtonProperty, value); }
		}

		public static readonly DependencyProperty EnableDetailsButtonProperty =
			DependencyProperty.Register("EnableDetailsButton", typeof(bool), typeof(ArticleDetailsControl), new PropertyMetadata(true));

		public ArticleDetailsControl()
		{
			InitializeComponent();

			rootElement.DataContext = this;
		}
	}
}
