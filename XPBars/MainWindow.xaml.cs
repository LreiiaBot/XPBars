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

            //Page insert = new Pages.InsertPage();
            //fInsert.Navigate(insert);
        }

        private void Expander_Collapsed(object sender, RoutedEventArgs e)
        {
        }

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
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
                //TreeView y = (TreeView)e.OriginalSource;
                //y.Visibility = Visibility.Hidden;

                //var z = y.Items;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var item = Mvm.LXPBars[0].Subbars[3].Subbars[1];
            Insertion i = new Insertion("DemoInsertion", 5, XPWeight.Great);
            item.AddValue(i);
        }
    }
}
