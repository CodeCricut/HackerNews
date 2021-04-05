using Hackernews.WPF.ApiClients;
using Hackernews.WPF.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hackernews.WPF
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindowViewModel MainWindowVM { get; }

		public MainWindow(IApiClient apiClient)
		{
			InitializeComponent();

			MainWindowVM = new MainWindowViewModel(apiClient);
			DataContext = MainWindowVM;

			this.Loaded += MainWindow_Loaded;
		}

		private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
		{
			await MainWindowVM.UserListViewModel.LoadUsersAsync();
			if (MainWindowVM.UserViewModel.TryLoadUserCommand.CanExecute(null))
				MainWindowVM.UserViewModel.TryLoadUserCommand.Execute(null);
		}
	}
}
