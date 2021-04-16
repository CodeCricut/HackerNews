using Hackernews.WPF.ApiClients;
using Hackernews.WPF.Helpers;
using Hackernews.WPF.Services;
using Hackernews.WPF.ViewModels;
using System;
using System.Windows;

namespace Hackernews.WPF
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindowViewModel MainWindowVM { get; }

		public MainWindow(IApiClient apiClient, PrivateUserViewModel userVm, ISignInManager signInManager)
		{
			InitializeComponent();

			MainWindowVM = new MainWindowViewModel(apiClient, userVm);
			MainWindowVM.CloseAction = () => {
				this.Close();
				Application.Current.Shutdown();
			};
			MainWindowVM.LogoutAction = async () =>
			{
				await signInManager.SignOutAsync();

				var newMainWindow = new MainWindow(apiClient, userVm, signInManager);
				LoginWindow loginWindow = new LoginWindow(signInManager, newMainWindow);

				loginWindow.Show();
				this.Close();
			};

			DataContext = MainWindowVM;

			this.Loaded += MainWindow_Loaded;
		}

		private void MainWindow_Loaded(object sender, RoutedEventArgs e)
		{
			MainWindowVM.SelectHome();
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
