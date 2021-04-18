using Hackernews.WPF.MVVM.ViewModel;
using System;
using System.Windows;
using System.Windows.Input;

namespace Hackernews.WPF
{
	/// <summary>
	/// Interaction logic for EntityHomeWindow.xaml
	/// </summary>
	public partial class EntityHomeWindow : Window
	{
		public bool IsClosed { get; private set; }

		public EntityHomeViewModel EntityHomeViewModel { get; }

		public EntityHomeWindow(EntityHomeViewModel entityHomeViewModel)
		{
			InitializeComponent();
			EntityHomeViewModel = entityHomeViewModel;

			rootElement.DataContext = this;
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
