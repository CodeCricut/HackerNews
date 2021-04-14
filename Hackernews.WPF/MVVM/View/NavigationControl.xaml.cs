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
	/// Interaction logic for NavigationControl.xaml
	/// </summary>
	public partial class NavigationControl : UserControl
	{
		public ICommand PrevPageCommand
		{
			get { return (ICommand)GetValue(PrevPageCommandProperty); }
			set { SetValue(PrevPageCommandProperty, value); }
		}
		public static readonly DependencyProperty PrevPageCommandProperty =
			DependencyProperty.Register("PrevPageCommand", typeof(ICommand), typeof(NavigationControl), new PropertyMetadata(default(ICommand)));


		public object PrevPageCommandParameter
		{
			get { return (object)GetValue(PrevPageCommandParameterProperty); }
			set { SetValue(PrevPageCommandParameterProperty, value); }
		}
		public static readonly DependencyProperty PrevPageCommandParameterProperty =
			DependencyProperty.Register("PrevPageCommandParameter", typeof(object), typeof(NavigationControl), new PropertyMetadata(null));

		public int CurrentPageNumber
		{
			get { return (int)GetValue(CurrentPageNumberProperty); }
			set { SetValue(CurrentPageNumberProperty, value); }
		}
		public static readonly DependencyProperty CurrentPageNumberProperty =
			DependencyProperty.Register("CurrentPageNumber", typeof(int), typeof(NavigationControl), new PropertyMetadata(0));

		public int TotalPageCount
		{
			get { return (int)GetValue(TotalPageCountProperty); }
			set { SetValue(TotalPageCountProperty, value); }
		}
		public static readonly DependencyProperty TotalPageCountProperty =
			DependencyProperty.Register("TotalPageCount", typeof(int), typeof(NavigationControl), new PropertyMetadata(0));

		public ICommand NextPageCommand
		{
			get { return (ICommand)GetValue(NextPageCommandProperty); }
			set { SetValue(NextPageCommandProperty, value); }
		}
		public static readonly DependencyProperty NextPageCommandProperty =
			DependencyProperty.Register("NextPageCommand", typeof(ICommand), typeof(NavigationControl), new PropertyMetadata(default(NavigationControl)));

		public object NextPageCommandParameter
		{
			get { return (object)GetValue(NextPageCommandParameterProperty); }
			set { SetValue(NextPageCommandParameterProperty, value); }
		}
		public static readonly DependencyProperty NextPageCommandParameterProperty =
			DependencyProperty.Register("NextPageCommandParameter", typeof(object), typeof(NavigationControl), new PropertyMetadata(default(object)));

		public NavigationControl()
		{
			InitializeComponent();

			rootElement.DataContext = this;
		}
	}
}
