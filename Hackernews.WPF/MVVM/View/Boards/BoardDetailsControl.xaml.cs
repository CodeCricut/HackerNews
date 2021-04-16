﻿using Hackernews.WPF.MVVM.ViewModel;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Hackernews.WPF.Controls
{
	/// <summary>
	/// Interaction logic for BoardDetailsControl.xaml
	/// </summary>
	public partial class BoardDetailsControl : UserControl
	{
		public BoardViewModel BoardViewModel
		{
			get { return (BoardViewModel)GetValue(BoardViewModelProperty); }
			set { SetValue(BoardViewModelProperty, value); }
		}

		public static readonly DependencyProperty BoardViewModelProperty =
			DependencyProperty.Register("BoardViewModel", typeof(BoardViewModel), typeof(BoardDetailsControl), new PropertyMetadata(default(BoardViewModel)));

		public BoardDetailsControl()
		{
			InitializeComponent();

			rootElement.DataContext = this;
		}
	}
}