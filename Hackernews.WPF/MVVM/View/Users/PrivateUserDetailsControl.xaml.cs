using Hackernews.WPF.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace Hackernews.WPF.MVVM.View.Users
{
	/// <summary>
	/// Interaction logic for PrivateUserDetailsControl.xaml
	/// </summary>
	public partial class PrivateUserDetailsControl : UserControl
	{
		public PrivateUserViewModel PrivateUserViewModel
		{
			get { return (PrivateUserViewModel)GetValue(PrivateUserViewModelProperty); }
			set { SetValue(PrivateUserViewModelProperty, value); }
		}

		public static readonly DependencyProperty PrivateUserViewModelProperty =
			DependencyProperty.Register("PrivateUserViewModel", typeof(PrivateUserViewModel), typeof(PrivateUserDetailsControl), new PropertyMetadata(default(PrivateUserViewModel)));

		public PrivateUserDetailsControl()
		{
			InitializeComponent();

			rootElement.DataContext = this;
		}
	}
}
