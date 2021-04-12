using Hackernews.WPF.Services;
using Hackernews.WPF.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace Hackernews.WPF
{
	/// <summary>
	/// Interaction logic for LoginWindow.xaml
	/// </summary>
	public partial class LoginWindow : Window
	{
		private LoginWindowViewModel ViewModel { get; }

		public LoginWindow(ISignInManager signInManager, MainWindow mainWindow)
		{
			InitializeComponent();

			ViewModel = new LoginWindowViewModel(signInManager, thisWindow: this, mainWindow);
			rootElement.DataContext = ViewModel;
		}

		private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
		{
			if (ViewModel != null)
				ViewModel.Password = ((PasswordBox)sender).SecurePassword;
		}
	}
}
