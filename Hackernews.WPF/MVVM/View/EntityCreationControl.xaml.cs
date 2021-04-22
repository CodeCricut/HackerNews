using Hackernews.WPF.MVVM.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace Hackernews.WPF.MVVM.View
{
	/// <summary>
	/// Interaction logic for EntityCreationControl.xaml
	/// </summary>
	public partial class EntityCreationControl : UserControl
	{
		public EntityCreationViewModel EntityCreationViewModel
		{
			get { return (EntityCreationViewModel)GetValue(EntityCreationViewModelProperty); }
			set { SetValue(EntityCreationViewModelProperty, value); }
		}
		public static readonly DependencyProperty EntityCreationViewModelProperty =
			DependencyProperty.Register("EntityCreationViewModel", typeof(EntityCreationViewModel), typeof(EntityCreationControl));


		public EntityCreationControl()
		{
			InitializeComponent();

			rootElement.DataContext = this;
		}
	}
}
