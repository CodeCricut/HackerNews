using Hackernews.WPF.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace Hackernews.WPF.Controls
{
	/// <summary>
	/// Interaction logic for UserDetailControl.xaml
	/// </summary>
	public partial class UserDetailControl : UserControl
	{
		public UserListViewModel UserListViewModel
		{
			get { return (UserListViewModel)GetValue(UserListViewModelProperty); }
			set { SetValue(UserListViewModelProperty, value); }
		}

		public static readonly DependencyProperty UserListViewModelProperty =
			DependencyProperty.Register("UserListViewModel", typeof(UserListViewModel), typeof(UserDetailControl), new PropertyMetadata(default(UserListViewModel)));

		public UserDetailControl()
		{
			InitializeComponent();

			rootElement.DataContext = this;
		}
	}
}
