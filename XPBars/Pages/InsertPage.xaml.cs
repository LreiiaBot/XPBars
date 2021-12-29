using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace XPBars.Pages
{
    /// <summary>
    /// Interaktionslogik für InsertPage.xaml
    /// </summary>
    public partial class InsertPage : Page
    {
        public InsertPage()
        {
            InitializeComponent();
            this.DataContext = (MainViewModel)FindResource("mvm");

            // determine possible values for orbs
            cbWeight.ItemsSource = Enum.GetValues(typeof(XPWeight));
            cbWeight.SelectedIndex = 0;
        }
    }
}
