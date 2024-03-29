﻿using Hackernews.WPF.ViewModels;
using HackerNews.WPF.Core.View;
using System.Windows;

namespace Hackernews.WPF
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window, IHaveViewModel<MainWindowViewModel>
	{

		public MainWindowViewModel MainWindowVM { get; private set; }

		public MainWindow()
		{
			InitializeComponent();

			DataContext = this;

			this.Loaded += MainWindow_Loaded;
		}

		public void SetViewModel(MainWindowViewModel viewModel)
		{
			MainWindowVM = viewModel;
		}

		private void MainWindow_Loaded(object sender, RoutedEventArgs e)
		{
			// TODO: make FullscreenVMSelectedMessage ???
			MainWindowVM.FullscreenVM.SelectHome();
			homeNavButton.IsChecked = true;
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
