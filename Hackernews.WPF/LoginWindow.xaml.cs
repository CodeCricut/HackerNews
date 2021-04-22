using Hackernews.WPF.ApiClients;
using Hackernews.WPF.Services;
using Hackernews.WPF.ViewModels;
using System.Windows;

namespace Hackernews.WPF
{
	/// <summary>
	/// Interaction logic for LoginWindow.xaml
	/// </summary>
	public partial class LoginWindow : Window
	{
		private LoginWindowViewModel ViewModel { get; }

		public LoginWindow(ISignInManager signInManager, IApiClient apiClient, MainWindow mainWindow)
		{
			InitializeComponent();

			ViewModel = new LoginWindowViewModel(signInManager, apiClient, thisWindow: this, mainWindow);
			ViewModel.CloseAction = () =>
			{
				this.Close();
				Application.Current.Shutdown();
			};

			rootElement.DataContext = ViewModel;
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
