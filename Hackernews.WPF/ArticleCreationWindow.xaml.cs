using Hackernews.WPF.MVVM.ViewModel.Articles;
using HackerNews.WPF.Core.View;
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
using System.Windows.Shapes;

namespace Hackernews.WPF
{
	/// <summary>
	/// Interaction logic for ArticleCreationWindow.xaml
	/// </summary>
	public partial class ArticleCreationWindow : Window, IHaveViewModel<ArticleCreationViewModel>
	{
		public ArticleCreationViewModel ArticleCreationViewModel { get; private set; }
		public ArticleCreationWindow()
		{
			InitializeComponent();

			rootElement.DataContext = this;
		}

		public void SetViewModel(ArticleCreationViewModel viewModel)
		{
			ArticleCreationViewModel = viewModel;
		}

		private void dragPanel_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			if (e.ChangedButton == System.Windows.Input.MouseButton.Left)
			{
				this.DragMove();
			}
		}
	}
}
