using Hackernews.WPF.MVVM.ViewModel;
using Hackernews.WPF.MVVM.ViewModel.Common;
using HackerNews.Domain.Common.Models.Users;
using System.Windows;
using System.Windows.Controls;

namespace Hackernews.WPF.Controls
{
	/// <summary>
	/// Interaction logic for UserListControl.xaml
	/// </summary>
	public partial class UserListControl : UserControl
	{
		public EntityListViewModel<PublicUserViewModel, GetPublicUserModel> UserListViewModel
		{
			get { return (EntityListViewModel<PublicUserViewModel, GetPublicUserModel>)GetValue(UserListViewModelProperty); }
			set { SetValue(UserListViewModelProperty, value); }
		}

		public static readonly DependencyProperty UserListViewModelProperty =
			DependencyProperty.Register("UserListViewModel", typeof(EntityListViewModel<PublicUserViewModel, GetPublicUserModel>), typeof(UserListControl));

		public UserListControl()
		{
			InitializeComponent();

			rootElement.DataContext = this;
		}
	}
}
