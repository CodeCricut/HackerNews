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
			DataContext = MainWindowVM;

			this.Loaded += MainWindow_Loaded;
		}

		private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
		{
			await Task.Factory.StartNew(() => MainWindowVM.UserListViewModel.LoadCommand.TryExecute());
			if (MainWindowVM.PrivateUserViewModel.TryLoadUserCommand.CanExecute(null))
				MainWindowVM.PrivateUserViewModel.TryLoadUserCommand.Execute(null);
		}
	}
}
