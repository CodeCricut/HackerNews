using Hackernews.WPF.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
