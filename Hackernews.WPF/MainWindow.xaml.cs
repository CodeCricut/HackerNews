using Hackernews.WPF.ApiClients;
using Hackernews.WPF.Services;
using Hackernews.WPF.ViewModels;
using HackerNews.WPF.MessageBus.Application;
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
		private readonly IEventAggregator _ea;

		public MainWindowViewModel MainWindowVM { get; }

		public MainWindow(IEventAggregator ea, 
			MainWindowViewModel mainWindowVM, 
			ISignInManager signInManager)
		{
			InitializeComponent();

			_ea = ea;
			MainWindowVM = mainWindowVM;

			DataContext = MainWindowVM;

			ea.RegisterHandler<CloseMainWindowMessage>(CloseApp);
			ea.RegisterHandler<LogoutRequestedMessage>(CloseWindow);

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

		private void CloseApp(CloseMainWindowMessage msg)
		{
			_ea.SendMessage(new CloseApplicationMessage());
		}

		private void CloseWindow(LogoutRequestedMessage msg)
		{
			// In order to prevent a memory leak, this short-living subscriber must unsubscribe from potentially long-living publishers.
			_ea.UnregisterHandler<CloseMainWindowMessage>(CloseApp);
			_ea.UnregisterHandler<LogoutRequestedMessage>(CloseWindow);

			this.Close();
		}
	}
}
