using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace XPBars
{
    public class CustomExpanderControl : Expander
    {
        public static readonly DependencyProperty SelectedDependencyProperty = DependencyProperty.Register("Selected", typeof(bool), typeof(CustomExpanderControl), new FrameworkPropertyMetadata(false, new PropertyChangedCallback(SelectedPropertyChanged)));

        public bool Selected
        {
            get { return (bool)GetValue(SelectedDependencyProperty); }
            set { SetValue(SelectedDependencyProperty, value); }
        }

        public static void SelectedPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            if (args.NewValue == null)
            {
                return;
            }
            var x = (bool)args.NewValue;
            if (x)
            {
                ((CustomExpanderControl)sender).Visibility = Visibility.Visible;
            }
            else
            {
                ((CustomExpanderControl)sender).Visibility = Visibility.Collapsed;
            }
        }
        public CustomExpanderControl()
        {
            Visibility = Visibility.Collapsed;
        }
    }
}
