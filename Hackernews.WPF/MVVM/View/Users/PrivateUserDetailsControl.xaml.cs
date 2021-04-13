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

namespace Hackernews.WPF.MVVM.View.Users
{
	/// <summary>
	/// Interaction logic for PrivateUserDetailsControl.xaml
	/// </summary>
	public partial class PrivateUserDetailsControl : UserControl
	{
		public PrivateUserViewModel PrivateUserViewModel
		{
			get { return (PrivateUserViewModel)GetValue(PrivateUserViewModelProperty); }
			set { SetValue(PrivateUserViewModelProperty, value); }
		}

		public static readonly DependencyProperty PrivateUserViewModelProperty =
			DependencyProperty.Register("PrivateUserViewModel", typeof(PrivateUserViewModel), typeof(PrivateUserDetailsControl), new PropertyMetadata(default(PrivateUserViewModel)));

		public PrivateUserDetailsControl()
		{
			InitializeComponent();

			rootElement.DataContext = this;
		}
	}
}
