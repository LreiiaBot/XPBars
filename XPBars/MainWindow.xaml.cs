using System.Windows;
using System.Windows.Controls;

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
    }
}
