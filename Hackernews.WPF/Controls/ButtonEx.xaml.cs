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

namespace Hackernews.WPF.Controls
{
    /// <summary>
    /// Standard button with extensions
    /// </summary>
    public partial class ButtonEx : Button
    {

        public ButtonEx()
        {
            InitializeComponent();
        }

        #region HoverBackground
        readonly static Brush DefaultHoverBackgroundValue = new BrushConverter().ConvertFromString("#FFBEE6FD") as Brush;

        public Brush HoverBackground
        {
            get { return (Brush)GetValue(HoverBackgroundProperty); }
            set { SetValue(HoverBackgroundProperty, value); }
        }
        public static readonly DependencyProperty HoverBackgroundProperty = DependencyProperty.Register(
          "HoverBackground", typeof(Brush), typeof(ButtonEx), new PropertyMetadata(DefaultHoverBackgroundValue));
		#endregion

		#region HoverBorderColor
		readonly static Brush DefaultHoverBorderColorValue = new BrushConverter().ConvertFromString("Transparent") as Brush;
        public Brush HoverBorderColor
		{
			get { return (Brush)GetValue(HoverBorderColorProperty); }
			set { SetValue(HoverBorderColorProperty, value); }
		}

		public static readonly DependencyProperty HoverBorderColorProperty =
			DependencyProperty.Register("HoverBorderColor", typeof(Brush), typeof(ButtonEx), new PropertyMetadata(DefaultHoverBorderColorValue));
        #endregion

        #region HoverFontSize
        readonly static double DefaultHoverFontSize = (double) new FontSizeConverter().ConvertFromString("12");
		
        public double HoverFontSize
		{
			get { return (double)GetValue(HoverFontSizeProperty); }
			set { SetValue(HoverFontSizeProperty, value); }
		}

		// Using a DependencyProperty as the backing store for HoverFontSize.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty HoverFontSizeProperty =
			DependencyProperty.Register("HoverFontSize", typeof(double), typeof(ButtonEx), new PropertyMetadata(DefaultHoverFontSize));
		#endregion
	}
}
