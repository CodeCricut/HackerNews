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
		private readonly ISignInManager _signInManager;
		private readonly LoginWindow _loginWindow;

		public MainWindowViewModel MainWindowVM { get; }

		public MainWindow(IEventAggregator ea, 
			MainWindowViewModel mainWindowVM, 
			LoginWindow loginWindow,
			ISignInManager signInManager)
		{
			InitializeComponent();

			_loginWindow = loginWindow;
			_signInManager = signInManager;

			MainWindowVM = mainWindowVM;
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

			_loginWindow.Show();
			this.Close();
		}
	}
}
