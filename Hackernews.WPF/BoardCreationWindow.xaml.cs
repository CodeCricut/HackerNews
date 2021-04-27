using Hackernews.WPF.MVVM.ViewModel.Boards;
using HackerNews.WPF.Core.View;
using System.Windows;

namespace Hackernews.WPF
{
	/// <summary>
	/// Interaction logic for BoardCreationWindow.xaml
	/// </summary>
	public partial class BoardCreationWindow : Window, IHaveViewModel<BoardCreationViewModel>
	{
		public BoardCreationViewModel BoardCreationViewModel { get; private set; }

		public BoardCreationWindow()
		{
			InitializeComponent();

			rootElement.DataContext = this;
		}

		public void SetViewModel(BoardCreationViewModel viewModel)
		{
			BoardCreationViewModel = viewModel;
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
