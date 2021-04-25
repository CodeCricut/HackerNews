using Hackernews.WPF.Core;
using Hackernews.WPF.ViewModels;
using HackerNews.WPF.MessageBus.Core;
using System.Windows;

namespace Hackernews.WPF
{
	/// <summary>
	/// Interaction logic for LoginWindow.xaml
	/// </summary>
	public partial class LoginWindow : Window, IHaveViewModel<LoginWindowViewModel>
	{
		private readonly IEventAggregator _ea;

		public LoginWindowViewModel ViewModel { get; private set; }

		public LoginWindow(IEventAggregator ea)
		{
			InitializeComponent();
			_ea = ea;

			//ea.RegisterHandler<CloseLoginWindowMessage>(CloseWindow);

			rootElement.DataContext = this;
		}


		public void SetViewModel(LoginWindowViewModel viewModel)
		{
			ViewModel = viewModel;
		}

		private void dragPanel_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			if (e.ChangedButton == System.Windows.Input.MouseButton.Left)
			{
				this.DragMove();
			}
		}

		//private void CloseWindow(CloseLoginWindowMessage msg)
		//{
		//	// In order to prevent a memory leak, this short-living subscriber must unsubscribe from potentially long-living publishers.
		//	_ea.UnregisterHandler<CloseLoginWindowMessage>(CloseWindow);
		//	this.Close();
		//}

	}
}
