using Hackernews.WPF.Core;
using Hackernews.WPF.MVVM.ViewModel;
using System.Windows;

namespace Hackernews.WPF
{
	/// <summary>
	/// Interaction logic for EntityCreationWindow.xaml
	/// </summary>
	public partial class EntityCreationWindow : Window, IHaveViewModel<EntityCreationViewModel>
	{
		public EntityCreationViewModel EntityCreationViewModel { get; private set; }

		public EntityCreationWindow()
		{
			InitializeComponent();

			DataContext = this;
		}

		public void SetViewModel(EntityCreationViewModel viewModel)
		{
			EntityCreationViewModel = viewModel;
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
