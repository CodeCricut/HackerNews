using Hackernews.WPF.Controls;
using Hackernews.WPF.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
	/// Interaction logic for MultiArticleDetailsControl.xaml
	/// </summary>
	public partial class MultiArticleDetailsControl : UserControl
	{
		public ObservableCollection<ArticleViewModel> ArticleViewModels
		{
			get { return (ObservableCollection<ArticleViewModel>)GetValue(ArticleViewModelsProperty); }
			set { SetValue(ArticleViewModelsProperty, value); }
		}

		public static readonly DependencyProperty ArticleViewModelsProperty =
			DependencyProperty.Register("ArticleViewModels", typeof(ObservableCollection<ArticleViewModel>), typeof(MultiArticleDetailsControl), new PropertyMetadata(default(ObservableCollection<ArticleViewModel>)));


		public MultiArticleDetailsControl()
		{
			InitializeComponent();

			rootElement.DataContext = this;
		}
	}
}
