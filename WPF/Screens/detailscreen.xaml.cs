using Database.Model;
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
using System.Diagnostics;
using WPF.Pages;

namespace WPF
{
    /// <summary>
    /// Interaction logic for detailscreen.xaml
    /// </summary>
    public partial class Detailscreen : Window
    {
        public string studentnr;
        Student selectedStudent;
        // Keep track of open page
        // 0 is details page
        // 1 is problem page
        // 2 is leerdoelen page
        private int screen = 0;
        public Detailscreen(Student st)
        {
            InitializeComponent();
            selectedStudent = st;
            StudentdetailsPage pg = new StudentdetailsPage(selectedStudent);
            pg.addStudentInfo();
            DetailFrame.NavigationService.Navigate(pg);
        }

        private void backButton(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Overzichtscreen(object sender, RoutedEventArgs e)
        {
            if (screen == 0) return;
            screen = 0;
            StudentdetailsPage pg = new StudentdetailsPage(selectedStudent);
            pg.addStudentInfo();
            DetailFrame.NavigationService.Navigate(pg);
        }
        private void planMeeting(object sender, RoutedEventArgs e)
        {

            Inplannen inplannen = new Inplannen(selectedStudent);
            inplannen.studentnr = studentnr;
            inplannen.ShowDialog();
        }

        private void ProblemBtn(object sender, RoutedEventArgs e)
        {
            if (screen == 1) return;
            screen = 1;
            DetailscreenProblems detailscreenProblems = new(selectedStudent);
            DetailFrame.NavigationService.Navigate(detailscreenProblems);


        }

        private void LeerdoelenList(object sender, RoutedEventArgs e)
        { 
            if(screen == 2) return;
            screen = 2;
            ShowStudentLeerdoelenTable StudentLeerdoelenTable = new(selectedStudent);
            DetailFrame.NavigationService.Navigate(StudentLeerdoelenTable);
        }

        
    }
}
 