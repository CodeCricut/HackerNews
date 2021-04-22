using Hackernews.WPF.MVVM.ViewModel;
using System;
using System.Windows;

namespace Hackernews.WPF
{
	/// <summary>
	/// Interaction logic for EntityCreationWindow.xaml
	/// </summary>
	public partial class EntityCreationWindow : Window
	{
		public EntityCreationViewModel EntityCreationViewModel { get; }

		public bool IsClosed { get; private set; }

		public EntityCreationWindow(EntityCreationViewModel entityCreationViewModel)
		{
			InitializeComponent();
			EntityCreationViewModel = entityCreationViewModel;

			DataContext = this;
		}

		private void dragPanel_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			if (e.ChangedButton == System.Windows.Input.MouseButton.Left)
			{
				this.DragMove();
			}
		}

		protected override void OnClosed(EventArgs e)
		{
			base.OnClosed(e);

			IsClosed = true;
		}
	}
}
