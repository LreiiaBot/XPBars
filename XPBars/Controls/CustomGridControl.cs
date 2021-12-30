using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace XPBars
{
    class CustomGridControl : Grid
    {
        public static readonly DependencyProperty SelectedDependencyProperty = DependencyProperty.Register("Selected", typeof(bool), typeof(CustomGridControl), new FrameworkPropertyMetadata(false, new PropertyChangedCallback(SelectedPropertyChanged)));

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
                ((CustomGridControl)sender).Visibility = Visibility.Visible;
            }
            else
            {
                ((CustomGridControl)sender).Visibility = Visibility.Collapsed;
            }
        }
        public CustomGridControl()
        {
            Visibility = Visibility.Collapsed;
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            ComboBox comboBox = null;
            foreach (var child in this.Children)
            {
                comboBox = child as ComboBox;
                if (comboBox != null)
                {
                    // determine possible values for orbs
                    comboBox.ItemsSource = Enum.GetValues(typeof(XPWeight));
                    comboBox.SelectedIndex = 0;
                }
            }
        }
    }
}
