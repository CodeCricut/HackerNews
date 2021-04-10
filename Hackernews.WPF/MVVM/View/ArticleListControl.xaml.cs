using Hackernews.WPF.ViewModels;
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

namespace Hackernews.WPF.Controls
{
	/// <summary>
	/// Interaction logic for ArticleListControl.xaml
	/// </summary>
	public partial class ArticleListControl : UserControl
	{
		public ArticlesViewModel ArticlesViewModel
		{
			get { return (ArticlesViewModel)GetValue(ArticlesViewModelProperty); }
			set { SetValue(ArticlesViewModelProperty, value); }
		}

		public static readonly DependencyProperty ArticlesViewModelProperty =
			DependencyProperty.Register("ArticlesViewModel", typeof(ArticlesViewModel), typeof(ArticleListControl));



		public ArticleListControl()
		{
			InitializeComponent();

			rootElement.DataContext = this;
		}
	}
}
