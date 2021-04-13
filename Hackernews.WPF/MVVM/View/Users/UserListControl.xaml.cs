using Hackernews.WPF.MVVM.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace Hackernews.WPF.Controls
{
	/// <summary>
	/// Interaction logic for UserListControl.xaml
	/// </summary>
	public partial class UserListControl : UserControl
	{
		public UserListViewModel UserListViewModel
		{
			get { return (UserListViewModel)GetValue(UserListViewModelProperty); }
			set { SetValue(UserListViewModelProperty, value); }
		}

		// Using a DependencyProperty as the backing store for UserListViewModel.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty UserListViewModelProperty =
			DependencyProperty.Register("UserListViewModel", typeof(UserListViewModel), typeof(UserListControl), new PropertyMetadata(default(UserListViewModel)));

		public UserListControl()
		{
			InitializeComponent();

			rootElement.DataContext = this;
		}
	}
}
