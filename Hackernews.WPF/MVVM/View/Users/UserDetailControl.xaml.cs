using Hackernews.WPF.MVVM.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace Hackernews.WPF.Controls
{
	/// <summary>
	/// Interaction logic for UserDetailControl.xaml
	/// </summary>
	public partial class UserDetailControl : UserControl
	{
		public PublicUserViewModel UserViewModel
		{
			get { return (PublicUserViewModel)GetValue(UserViewModelProperty); }
			set { SetValue(UserViewModelProperty, value); }
		}

		public static readonly DependencyProperty UserViewModelProperty =
			DependencyProperty.Register("UserViewModel", typeof(PublicUserViewModel), typeof(UserDetailControl), new PropertyMetadata(default(PublicUserViewModel)));


		public UserDetailControl()
		{
			InitializeComponent();

			rootElement.DataContext = this;
		}
	}
}
