using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Windows;

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
            //ExcelImportWindow excelImportWindow = new ExcelImportWindow();
            //excelImportWindow.Show();
            //new ExcelImportWindow().Show();
        }

        private void Click(object sender, RoutedEventArgs e)
        {
            ShowStudentTable win2 = new ShowStudentTable();
            win2.Show();
        }

        private void ButtonPlanIn_Click(object sender, RoutedEventArgs e)
        {
            Inplannen popup = new Inplannen();
            popup.ShowDialog();
        }

        private void FrameworkElement_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            Console.WriteLine("HELLO WORLD!");
        }

        private void ButtonImportStudents_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel Files(.xlsx)|*.xlsx";
            openFileDialog.ShowDialog();

            string directory = openFileDialog.FileName;
            Trace.WriteLine(directory);

            ExcelImporter.ImportStudentsFromFile(directory);
            ExcelImporter.PrintStudents();
        }
    }
}