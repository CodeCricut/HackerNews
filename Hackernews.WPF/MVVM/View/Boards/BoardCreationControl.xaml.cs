using Hackernews.WPF.MVVM.ViewModel.Boards;
using System.Windows;
using System.Windows.Controls;

namespace Hackernews.WPF.MVVM.View.Boards
{
	/// <summary>
	/// Interaction logic for BoardCreationControl.xaml
	/// </summary>
	public partial class BoardCreationControl : UserControl
	{
		public BoardCreationViewModel BoardCreationViewModel
		{
			get { return (BoardCreationViewModel)GetValue(BoardCreationViewModelProperty); }
			set { SetValue(BoardCreationViewModelProperty, value); }
		}

		public static readonly DependencyProperty BoardCreationViewModelProperty =
			DependencyProperty.Register("BoardCreationViewModel", typeof(BoardCreationViewModel), typeof(BoardCreationControl), new PropertyMetadata(default(BoardCreationViewModel)));



		public BoardCreationControl()
		{
			InitializeComponent();

			rootElement.DataContext = this;
		}
	}
}
