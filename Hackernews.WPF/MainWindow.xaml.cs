using Hackernews.WPF.ApiClients;
using Hackernews.WPF.Services;
using Hackernews.WPF.ViewModels;
using HackerNews.WPF.MessageBus.Core;
using HackerNews.WPF.MessageBus.ViewModel.MainWindow;
using HackerNews.WPF.MessageBus.ViewModel.MainWindow.Profile;
using System.Threading.Tasks;
using System.Windows;

namespace Hackernews.WPF
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private readonly IApiClient _apiClient;
		private readonly ISignInManager _signInManager;

		public MainWindowViewModel MainWindowVM { get; }

		public MainWindow(IEventAggregator ea, 
			MainWindowViewModel mainWindowVM, 
			IApiClient apiClient,
			ISignInManager signInManager)
		{
			InitializeComponent();

			_signInManager = signInManager;

			MainWindowVM = mainWindowVM;
			_apiClient = apiClient;
			DataContext = MainWindowVM;

			ea.RegisterHandler<CloseMainWindowMessage>(msg => CloseApp());
			ea.RegisterHandler<LogoutRequestedMessage>(async msg => await Logout());

			this.Loaded += MainWindow_Loaded;
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

		private void CloseApp()
		{
			this.Close();
			Application.Current.Shutdown();
		}

		private async Task Logout()
		{
			await _signInManager.SignOutAsync();

			// Could use a factory instead to pass this as arg
			LoginWindow loginWindow = new LoginWindow(_signInManager, _apiClient, this);
			loginWindow.Show();
			this.Close();
		}
	}
}
