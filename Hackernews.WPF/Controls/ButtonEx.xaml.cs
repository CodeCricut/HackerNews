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
        readonly static Brush DefaultHoverBackgroundValue = new BrushConverter().ConvertFromString("#FFBEE6FD") as Brush;

        public ButtonEx()
        {
            InitializeComponent();
        }

        public Brush HoverBackground
        {
            get { return (Brush)GetValue(HoverBackgroundProperty); }
            set { SetValue(HoverBackgroundProperty, value); }
        }
        public static readonly DependencyProperty HoverBackgroundProperty = DependencyProperty.Register(
          "HoverBackground", typeof(Brush), typeof(ButtonEx), new PropertyMetadata(DefaultHoverBackgroundValue));
    }
}
