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
		public ArticleListViewModel ArticleListViewModel
		{
			get { return (ArticleListViewModel)GetValue(ArticleListViewModelProperty); }
			set { SetValue(ArticleListViewModelProperty, value); }
		}

		public static readonly DependencyProperty ArticleListViewModelProperty =
			DependencyProperty.Register(nameof(ArticleListViewModel), typeof(ArticleListViewModel), typeof(ArticleDetailsControl), new PropertyMetadata(default(ArticleListViewModel)));

		public ArticleDetailsControl()
		{
			InitializeComponent();
			
			rootElement.DataContext = this;
		}
	}
}
