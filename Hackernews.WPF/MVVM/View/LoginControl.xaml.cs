using Hackernews.WPF.MVVM.ViewModel;
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

namespace Hackernews.WPF.MVVM.View
{
	/// <summary>
	/// Interaction logic for LoginControl.xaml
	/// </summary>
	public partial class LoginControl : UserControl
	{
		public LoginViewModel LoginViewModel
		{
			get { return (LoginViewModel)GetValue(LoginViewModelProperty); }
			set { SetValue(LoginViewModelProperty, value); }
		}
		public static readonly DependencyProperty LoginViewModelProperty =
			DependencyProperty.Register("LoginViewModel", typeof(LoginViewModel), typeof(LoginControl), new PropertyMetadata(default(LoginViewModel)));

		public LoginControl()
		{
			InitializeComponent();

			rootElement.DataContext = this;
		}

		private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
		{
			if (LoginViewModel != null)
				LoginViewModel.Password = ((PasswordBox)sender).SecurePassword;
		}
	}
}
