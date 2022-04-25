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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WPF
{
    /// <summary>
    /// Interaction logic for ExcelImportWindow.xaml
    /// </summary>
    public partial class ExcelImportWindow : Window
    {
        public ExcelImportWindow()
        {
            InitializeComponent();
            Number.Content = 0;
        }

        private void OnImportButtonClick(object sender, RoutedEventArgs e)
        {
            Dispatcher.BeginInvoke(
                DispatcherPriority.Render,
                new Action(() =>
                    {
                        Importer.ImportStudentsFromFile();
                        //Number.Content = Importer.students.Length;
                    }
                ));
        }
    }
}
