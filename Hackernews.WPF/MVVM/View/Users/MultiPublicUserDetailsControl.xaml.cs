using Hackernews.WPF.MVVM.ViewModel;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Hackernews.WPF.MVVM.View.Users
{
	/// <summary>
	/// Interaction logic for MultiPublicUserDetailsControl.xaml
	/// </summary>
	public partial class MultiPublicUserDetailsControl : UserControl
	{
		public ObservableCollection<PublicUserViewModel> UserViewModels
		{
			get { return (ObservableCollection<PublicUserViewModel>)GetValue(UserViewModelsProperty); }
			set { SetValue(UserViewModelsProperty, value); }
		}

		public static readonly DependencyProperty UserViewModelsProperty =
			DependencyProperty.Register("UserViewModels", typeof(ObservableCollection<PublicUserViewModel>), typeof(MultiPublicUserDetailsControl), new PropertyMetadata(default(ObservableCollection<PublicUserViewModel>)));

		public MultiPublicUserDetailsControl()
		{
			InitializeComponent();

			rootElement.DataContext = this;
		}
	}
}
