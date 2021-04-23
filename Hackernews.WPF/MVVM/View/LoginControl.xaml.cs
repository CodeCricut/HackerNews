using Hackernews.WPF.MVVM.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace Hackernews.WPF.MVVM.View
{
	/// <summary>
	/// Interaction logic for LoginControl.xaml
	/// </summary>
	public partial class LoginControl : UserControl
	{
		public LoginModelViewModel LoginViewModel
		{
			get { return (LoginModelViewModel)GetValue(LoginViewModelProperty); }
			set { SetValue(LoginViewModelProperty, value); }
		}
		public static readonly DependencyProperty LoginViewModelProperty =
			DependencyProperty.Register("LoginViewModel", typeof(LoginModelViewModel), typeof(LoginControl), new PropertyMetadata(default(LoginModelViewModel)));

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
