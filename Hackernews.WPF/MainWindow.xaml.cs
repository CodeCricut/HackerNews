using Hackernews.WPF.ApiClients;
using Hackernews.WPF.ViewModels;
using System.Threading.Tasks;
using System.Windows;

namespace Hackernews.WPF
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindowViewModel MainWindowVM { get; }

		public MainWindow(IApiClient apiClient, PrivateUserViewModel userVm)
		{
			InitializeComponent();

			MainWindowVM = new MainWindowViewModel(apiClient, userVm);
			MainWindowVM.CloseAction = () => this.Close();

			DataContext = MainWindowVM;

			this.Loaded += MainWindow_Loaded;
		}

		private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
		{
			MainWindowVM.SelectHome();
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
