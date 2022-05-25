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

namespace WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void Click(object sender, RoutedEventArgs e)
        {
            ShowStudentTable win2 = new ShowStudentTable();
            win2.Show();
        }
        
        private void ClickB(object sender, RoutedEventArgs e)
        {
            ShowStudentTableBegeleider win3 = new ShowStudentTableBegeleider();
            win3.Show();
        }
    }
}
