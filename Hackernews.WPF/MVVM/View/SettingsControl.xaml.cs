using Hackernews.WPF.MVVM.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace Hackernews.WPF.MVVM.View
{
	public partial class SettingsControl : UserControl
	{
		public SettingsViewModel SettingsViewModel
		{
			get { return (SettingsViewModel)GetValue(SettingsViewModelProperty); }
			set { SetValue(SettingsViewModelProperty, value); }
		}

		public static readonly DependencyProperty SettingsViewModelProperty =
			DependencyProperty.Register("SettingsViewModel", typeof(SettingsViewModel), typeof(SettingsControl), new PropertyMetadata(default(SettingsViewModel)));



		public SettingsControl()
		{
			InitializeComponent();

			rootElement.DataContext = this;
		}
	}
}
