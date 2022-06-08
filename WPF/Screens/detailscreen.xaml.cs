using Database.Model;
using System.Windows;
using WPF.Pages;

namespace WPF
{
    /**
     * <summary>Interaction logic for detailscreen.xaml</summary>
     */
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

        /**
         * <summary>Button to go back to the detailscherm</summary>
         */
        private void backButton(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        
        /**
         * <summary>Show the overzicht screen</summary>
         */
        private void Overzichtscreen(object sender, RoutedEventArgs e)
        {
            // check if not already on this screen
            if (screen == 0) return;
            screen = 0;
            // create and goto page
            StudentdetailsPage pg = new(selectedStudent);
            pg.addStudentInfo();
            DetailFrame.NavigationService.Navigate(pg);
        }

        /**
         * <summary>Plan a meeting</summary>
         */
        private void planMeeting(object sender, RoutedEventArgs e)
        {
            //open screen to plan a meeting
            Inplannen inplannen = new(selectedStudent);
            inplannen.studentnr = studentnr;
            inplannen.ShowDialog();
        }

        /**
         * <summary>Show the problems screen</summary>
         */
        private void ProblemBtn(object sender, RoutedEventArgs e)
        {
            // check if not already on this screen
            if (screen == 1) return;
            screen = 1;
            // create and goto page
            DetailscreenProblems detailscreenProblems = new(selectedStudent);
            DetailFrame.NavigationService.Navigate(detailscreenProblems);


        }

        /**
         * <summary>Show the leerdoelen screen</summary>
         */
        private void LeerdoelenList(object sender, RoutedEventArgs e)
        {
            // check if not already on this screen
            if (screen == 2) return;
            screen = 2;
            // create and goto page
            ShowStudentLeerdoelenTable StudentLeerdoelenTable = new(selectedStudent);
            DetailFrame.NavigationService.Navigate(StudentLeerdoelenTable);
        }

        
    }
}
 