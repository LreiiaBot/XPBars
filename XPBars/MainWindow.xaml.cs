using System;
using System.Collections.Generic;
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

            //btnBin.Content = "\u047C";
            //btnBin.Content = "⌛";

            //btnFreeze.Content = "&#xf06d;";
            //btnFreeze.Content = "&#xf46a;";
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
        private void BtnOrderAZ(object sender, RoutedEventArgs e)
        {
            Mvm.OrderAllAZ();
        }
        private void BtnOrderZA(object sender, RoutedEventArgs e)
        {
            Mvm.OrderAllZA();
        }
        private void BtnOrder1N(object sender, RoutedEventArgs e)
        {
            Mvm.OrderAll1N();
        }
        private void BtnOrderN1(object sender, RoutedEventArgs e)
        {
            Mvm.OrderAllN1();
        }
        private void BtnExpand(object sender, RoutedEventArgs e)
        {
            foreach (var item in tvXPBars.Items)
            {
                var tvi = tvXPBars.ItemContainerGenerator.ContainerFromItem(item) as TreeViewItem;
                tvi?.ExpandSubtree();
            }
        }
        private void BtnDel(object sender, RoutedEventArgs e)
        {
            if (Mvm.SelectedXPBar == null)
            {
                return;
            }
            MessageBoxResult result = MessageBox.Show($"Do you want to delete XPBar '{Mvm.SelectedXPBar.Description}' permanently?", $"Delte {Mvm.SelectedXPBar.Description}", MessageBoxButton.YesNo, MessageBoxImage.Exclamation, MessageBoxResult.No);
            if (result == MessageBoxResult.Yes)
            {
                Mvm.DeleteBar();
            }
        }
        private void BtnFreeze(object sender, RoutedEventArgs e)
        {
            if (Mvm.SelectedXPBar == null)
            {
                return;
            }
            Mvm.ChangeFreezeState();
        }
    }
}
