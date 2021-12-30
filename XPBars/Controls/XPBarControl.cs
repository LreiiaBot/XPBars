using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Shapes;

namespace XPBars
{
    class XPBarControl : System.Windows.Controls.ProgressBar
    {
        public static readonly DependencyProperty LevelDependencyProperty = DependencyProperty.Register("Level", typeof(int), typeof(XPBarControl), new FrameworkPropertyMetadata(0, new PropertyChangedCallback(LevelPropertyChanged)));


        public string Level
        {
            get { return (string)GetValue(LevelDependencyProperty); }
            set { SetValue(LevelDependencyProperty, value); }
        }

        static void LevelPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            XPBarControl xpBar = (XPBarControl)sender;
            int newValue = (int)args.NewValue;
            int red = 40;
            int green = 120 + newValue * 4;
            int blue = 80;

            if (newValue < 50)
            {
                red = 5 + 5 * newValue;
                green = 155 + 2 * newValue;
                blue = 80; // maybe change a little bit play around find best
            }
            else if (newValue < 200)
            {
                // level 50 - 199
                // from 255, 255, 80 -> 255, 0, 0
                red = 255;
                double greenCalc = 255 - 1.275 * newValue + ((200 - newValue) * 4.925);
                green = (int)greenCalc;
                blue = 0;
            }
            else if (newValue < 1000)
            {
                // 200 to 1000
                // from 255, 0, 0 to 0, 0, 0
                red = (int)((1.0 - (((double)newValue - 200.0) / 800.0)) * 255.0); // linear gradient using percentage
                green = 0;
                blue = 0;
            }
            else
            {
                red = 0;
                green = 0;
                blue = 0;
            }

            // for safety if any value is exceeds byte -> set it to max
            if (red > 255)
            {
                red = 255;
            }
            if (green > 255)
            {
                green = 255;
            }
            if (blue > 255)
            {
                blue = 255;
            }

            // 0-50 -> from ? to 255(big influence - 5) 255(small influece - 2) 5$0$

            xpBar.Foreground = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, (byte)red, (byte)green, (byte)blue));
        }
    }
}
