using Hackernews.WPF.MVVM.ViewModel;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Hackernews.WPF.MVVM.View.Boards
{
	/// <summary>
	/// Interaction logic for MultiBoardDetailsControl.xaml
	/// </summary>
	public partial class MultiBoardDetailsControl : UserControl
	{
		public ObservableCollection<BoardViewModel> BoardViewModels
		{
			get { return (ObservableCollection<BoardViewModel>)GetValue(BoardViewModelsProperty); }
			set { SetValue(BoardViewModelsProperty, value); }
		}

		public static readonly DependencyProperty BoardViewModelsProperty =
			DependencyProperty.Register("BoardViewModels", typeof(ObservableCollection<BoardViewModel>), typeof(MultiBoardDetailsControl), new PropertyMetadata(default(ObservableCollection<BoardViewModel>)));

		public MultiBoardDetailsControl()
		{
			InitializeComponent();

			rootElement.DataContext = this;
		}
	}
}
