﻿using Hackernews.WPF.ViewModels;
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
