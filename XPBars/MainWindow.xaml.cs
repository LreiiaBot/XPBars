using System.Windows;
using System.Windows.Controls;

namespace XPBars
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Page insert = new Pages.InsertPage();
            fInsert.Navigate(insert);
        }
    }
}
