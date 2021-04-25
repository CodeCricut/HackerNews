using Hackernews.WPF.Core;
using Hackernews.WPF.MVVM.ViewModel;
using System.Windows;

namespace Hackernews.WPF
{
	/// <summary>
	/// Interaction logic for EntityHomeWindow.xaml
	/// </summary>
	public partial class EntityHomeWindow : Window, IHaveViewModel<EntityHomeViewModel>
	{
		public EntityHomeViewModel EntityHomeViewModel { get; private set; }

		public EntityHomeWindow()
		{
			InitializeComponent();

			rootElement.DataContext = this;
		}

		private void dragPanel_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			if (e.ChangedButton == System.Windows.Input.MouseButton.Left)
			{
				this.DragMove();
			}
		}

		public void SetViewModel(EntityHomeViewModel viewModel)
		{
			EntityHomeViewModel = viewModel;
		}
	}
}
