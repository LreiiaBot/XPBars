using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace XPBars
{
    class CustomTextBlockControl : TextBlock
    {
        public static readonly DependencyProperty LevelDependencyProperty = DependencyProperty.Register("Level", typeof(int), typeof(CustomTextBlockControl), new FrameworkPropertyMetadata(0, new PropertyChangedCallback(LevelPropertyChanged)));

        public int Level
        {
            get { return (int)GetValue(LevelDependencyProperty); }
            set { SetValue(LevelDependencyProperty, value); }
        }

        static void LevelPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            byte red = 0;
            byte green = 0;
            byte blue = 0;

            CustomTextBlockControl textBlock = (CustomTextBlockControl)sender;

            if (textBlock.Level >= 40 && textBlock.Level < 170)
            {
                red = green = blue = 80;
            }
            else if (textBlock.Level < 4600)
            {
                red = green = blue = 255;
            }
            else
            {
                red = green = blue = 0;
            }
            textBlock.Foreground = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, red, green, blue));
        }
    }
}
