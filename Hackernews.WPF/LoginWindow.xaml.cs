using Hackernews.WPF.ApiClients;
using Hackernews.WPF.Services;
using Hackernews.WPF.ViewModels;
using HackerNews.WPF.MessageBus.Core;
using HackerNews.WPF.MessageBus.ViewModel.LoginWindow;
using System.Windows;

namespace Hackernews.WPF
{
	/// <summary>
	/// Interaction logic for LoginWindow.xaml
	/// </summary>
	public partial class LoginWindow : Window
	{
		private readonly IEventAggregator _ea;

		private LoginWindowViewModel ViewModel { get; }

		public LoginWindow(IEventAggregator ea, LoginWindowViewModel loginWindowVM)
		{
			InitializeComponent();
			_ea = ea;
			ViewModel = loginWindowVM;

			ea.RegisterHandler<CloseLoginWindowMessage>(CloseWindow);

			rootElement.DataContext = ViewModel;
		}

		private void dragPanel_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			if (e.ChangedButton == System.Windows.Input.MouseButton.Left)
			{
				this.DragMove();
			}
		}

		private void CloseWindow(CloseLoginWindowMessage msg)
		{
			// In order to prevent a memory leak, this short-living subscriber must unsubscribe from potentially long-living publishers.
			//_ea.UnregisterHandler<CloseLoginWindowMessage>(CloseWindow);
			this.Close();
		}
	}
}
