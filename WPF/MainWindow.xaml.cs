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

        private void ButtonProblemSubmitting_Click(object sender, RoutedEventArgs e)
        {
            ProblemSubmitting popup = new ProblemSubmitting();
            popup.ShowDialog();
        }
    }
}