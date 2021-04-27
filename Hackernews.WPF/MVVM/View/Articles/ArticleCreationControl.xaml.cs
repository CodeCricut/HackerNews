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
	/// Interaction logic for ArticleCreationControl.xaml
	/// </summary>
	public partial class ArticleCreationControl : UserControl
	{
		public ArticleCreationViewModel ArticleCreationViewModel
		{
			get { return (ArticleCreationViewModel)GetValue(ArticleCreationViewModelProperty); }
			set { SetValue(ArticleCreationViewModelProperty, value); }
		}

		public static readonly DependencyProperty ArticleCreationViewModelProperty =
			DependencyProperty.Register("ArticleCreationViewModel", typeof(ArticleCreationViewModel), typeof(ArticleCreationControl), new PropertyMetadata(default(ArticleCreationViewModel)));

		public ArticleCreationControl()
		{
			InitializeComponent();

			rootElement.DataContext = this;
		}
	}
}
