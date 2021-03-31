using Hackernews.WPF.ViewModels;
using HackerNews.Domain.Common.Models.Articles;
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
	/// Interaction logic for ArticleDetailsControl.xaml
	/// </summary>
	public partial class ArticleDetailsControl : UserControl
	{
		public ArticlesViewModel ArticleViewModel
		{
			get { return (ArticlesViewModel)GetValue(ArticleViewModelProperty); }
			set { SetValue(ArticleViewModelProperty, value); }
		}

		public static readonly DependencyProperty ArticleViewModelProperty =
			DependencyProperty.Register("ArticleViewModel", typeof(ArticlesViewModel), typeof(ArticleDetailsControl), new PropertyMetadata(default(ArticlesViewModel)));

		public ArticleDetailsControl()
		{
			InitializeComponent();
			
			RootLayout.DataContext = this;
		}
	}
}
