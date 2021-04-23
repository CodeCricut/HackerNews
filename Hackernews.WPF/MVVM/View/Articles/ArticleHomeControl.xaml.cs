using Hackernews.WPF.MVVM.ViewModel.Articles;
using System;
using System.Collections.Generic;
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

namespace Hackernews.WPF.MVVM.View.Articles
{
	/// <summary>
	/// Interaction logic for ArticleHomeControl.xaml
	/// </summary>
	public partial class ArticleHomeControl : UserControl
	{
		public ArticleHomeViewModel ArticleHomeViewModel
		{
			get { return (ArticleHomeViewModel)GetValue(ArticleHomeViewModelProperty); }
			set { SetValue(ArticleHomeViewModelProperty, value); }
		}

		public static readonly DependencyProperty ArticleHomeViewModelProperty =
			DependencyProperty.Register("ArticleHomeViewModel", typeof(ArticleHomeViewModel), typeof(ArticleHomeControl));


		public ArticleHomeControl()
		{
			InitializeComponent();

			rootElement.DataContext = this;
		}
	}
}
