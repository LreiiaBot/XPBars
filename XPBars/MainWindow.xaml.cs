using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace XPBars
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainViewModel Mvm { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            Mvm = (MainViewModel)FindResource("mvm");
        }

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            Mvm.SelectedXPBar = (XPBar)e.NewValue;

            TreeView treeView = sender as TreeView;
            if (treeView != null)
            {
                var x = (XPBar)e.NewValue;
                if (x != null)
                {
                    x.Selected = true;
                }

                var y = (XPBar)e.OldValue;
                if (y != null)
                {
                    y.Selected = false;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //var item = Mvm.LXPBars[0].Subbars[3].Subbars[1];
            //Insertion i = new Insertion("DemoInsertion", 5, XPWeight.Great);
            //item.AddValue(i);

            Mvm.AddOrbs();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Mvm.Save();
        }

        private void TBAddInsertion(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                Mvm.AddOrbs();
            }
        }

        private void TBAddList(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                Mvm.AddBar();
            }
        }

        private void BtnAddList(object sender, RoutedEventArgs e)
        {
            Mvm.AddBar();
        }
    }
}
