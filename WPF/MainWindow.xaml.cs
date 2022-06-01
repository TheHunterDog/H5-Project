using System;
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
            Inplannen popup = new Inplannen(new Database.Model.Student());
            popup.ShowDialog();
        }

        private void FrameworkElement_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            Console.WriteLine("HELLO WORLD!");
        }

        private void ButtonAddSubject_Click(object sender, RoutedEventArgs e)
        {
            AddSubject win = new AddSubject();
            win.Show();
        }

        private void ButtonCoupleSubject_Click(object sender, RoutedEventArgs e)
        {
            SubjectAssigning win = new SubjectAssigning();
            win.Show();
        }

        
    }
}
