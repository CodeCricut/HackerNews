using Hackernews.WPF.MVVM.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace Hackernews.WPF.MVVM.View
{
	public partial class ProfileControl : UserControl
	{
		public ProfileViewModel ProfileViewModel
		{
			get { return (ProfileViewModel)GetValue(ProfileViewModelProperty); }
			set { SetValue(ProfileViewModelProperty, value); }
		}

		public static readonly DependencyProperty ProfileViewModelProperty =
			DependencyProperty.Register("ProfileViewModel", typeof(ProfileViewModel), typeof(ProfileControl), new PropertyMetadata(default(ProfileViewModel)));


		public ProfileControl()
		{
			InitializeComponent();

			rootElement.DataContext = this;
		}
	}
}
