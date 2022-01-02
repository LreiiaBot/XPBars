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
        public static readonly DependencyProperty CurrentValueDependencyProperty = DependencyProperty.Register("CurrentValue", typeof(int), typeof(CustomTextBlockControl), new FrameworkPropertyMetadata(0, new PropertyChangedCallback(CurrentValuePropertyChanged)));
        public static readonly DependencyProperty MaxValueDependencyProperty = DependencyProperty.Register("MaxValue", typeof(int), typeof(CustomTextBlockControl), new FrameworkPropertyMetadata(0, new PropertyChangedCallback(MaxValuePropertyChanged)));

        public int Level
        {
            get { return (int)GetValue(LevelDependencyProperty); }
            set { SetValue(LevelDependencyProperty, value); }
        }
        public int CurrentValue
        {
            get { return (int)GetValue(CurrentValueDependencyProperty); }
            set { SetValue(CurrentValueDependencyProperty, value); }
        }
        public int MaxValue
        {
            get { return (int)GetValue(MaxValueDependencyProperty); }
            set { SetValue(MaxValueDependencyProperty, value); }
        }

        static void LevelPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            ChangeColor(sender, args);
        }
        static void CurrentValuePropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            ChangeColor(sender, args);
        }
        static void MaxValuePropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            ChangeColor(sender, args);
        }

        static void ChangeColor(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            byte red = 0;
            byte green = 0;
            byte blue = 0;

            CustomTextBlockControl textBlock = (CustomTextBlockControl)sender;

            if (textBlock.Level >= 40 && textBlock.Level < 170)
            {
                int minXP = (int)Math.Round(4.02941 * textBlock.Level - 67.0);
                if (textBlock.CurrentValue >= minXP)
                {
                    red = green = blue = 120;
                }
                else
                {
                    red = green = blue = 255;
                }
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
