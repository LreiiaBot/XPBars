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

        public int Level
        {
            get { return (int)GetValue(LevelDependencyProperty); }
            set { SetValue(LevelDependencyProperty, value); }
        }

        public static readonly DependencyProperty FreezeDependencyProperty = DependencyProperty.Register("Freeze", typeof(bool), typeof(XPBarControl), new FrameworkPropertyMetadata(false, new PropertyChangedCallback(FreezePropertyChanged)));

        public bool Freeze
        {
            get { return (bool)GetValue(FreezeDependencyProperty); }
            set { SetValue(FreezeDependencyProperty, value); }
        }
        static void FreezePropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            LevelPropertyChanged(sender, args);
        }

        static void LevelPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            XPBarControl xpBar = (XPBarControl)sender;
            int newValue = xpBar.Level;
            int red = 40;
            int green = 120 + newValue * 4;
            int blue = 80;

            if (xpBar.Freeze)
            {
                // blue
                red = 190;
                green = 200;
                blue = 250;

                //red = 0;
                //green = 190;
                //blue = 255;
            }
            else if (newValue < 50)
            {
                red = 5 + 5 * newValue;
                //green = 155 + 2 * newValue;
                green = 205 + 1 * newValue;
                blue = 90; // maybe change a little bit play around find best
            }
            else if (newValue < 200)
            {
                // level 50 - 199
                // from 255, 255, 80 -> 255, 0, 0
                red = 255;
                double greenCalc = 255.0 - 1.275 * newValue + ((200.0 - newValue) * 4.925);
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
            else if (newValue < 5000)
            {
                // from 0 0 0 to 255 255 255
                red = (int)((((double)newValue - 1000.0) / 4000.0) * 255.0); // linear gradient using percentage
                green = red;
                blue = red;
            }
            else
            {
                red = 255;
                green = 255;
                blue = 255;
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

            if (red < 0)
            {
                red = 0;
            }
            if (green < 0)
            {
                green = 0;
            }
            if (blue < 0)
            {
                blue = 0;
            }

            // 0-50 -> from ? to 255(big influence - 5) 255(small influece - 2) 5$0$

            xpBar.Foreground = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, (byte)red, (byte)green, (byte)blue));
        }
    }
}
