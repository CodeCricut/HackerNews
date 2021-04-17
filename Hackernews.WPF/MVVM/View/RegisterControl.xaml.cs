using Hackernews.WPF.MVVM.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace Hackernews.WPF.MVVM.View
{
	/// <summary>
	/// Interaction logic for RegisterControl.xaml
	/// </summary>
	public partial class RegisterControl : UserControl
	{
		public RegisterViewModel RegisterViewModel
		{
			get { return (RegisterViewModel)GetValue(RegisterViewModelProperty); }
			set { SetValue(RegisterViewModelProperty, value); }
		}
		public static readonly DependencyProperty RegisterViewModelProperty =
			DependencyProperty.Register("RegisterViewModel", typeof(RegisterViewModel), typeof(RegisterControl), new PropertyMetadata(default(RegisterViewModel)));

		public RegisterControl()
		{
			InitializeComponent();

			rootElement.DataContext = this;
		}
	}
}
