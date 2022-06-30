#region

using System;
using System.Windows;
using Database.Model;
using WPF.Pages;

#endregion

namespace WPF.Screens
{
    /**
     * <summary>Interaction logic for detailscreen.xaml</summary>
     */
    public partial class Detailscreen : Window
    {
        public string studentnr;
        Student selectedStudent;

        private ScaleEngine _scaleEngine;
        // Keep track of open page
        // 0 is details page
        // 1 is problem page
        // 2 is leerdoelen page
        private int screen = 0;

        public Detailscreen(Student st)
        {
            _scaleEngine = MainWindow.GetScaleEngineInstance();
            InitializeComponent();
            DetailscreenWindow.MinHeight = _scaleEngine.MinHeight;
            DetailscreenWindow.MinWidth = _scaleEngine.MinWidth;

            DetailscreenWindow.Height = _scaleEngine.WindowHeight;
            DetailscreenWindow.Width = _scaleEngine.WindowWidth;
            
            selectedStudent = st;
            StudentDetailsPage pg = new StudentDetailsPage(selectedStudent);
            
            pg.AddStudentInfo();
            DetailFrame.NavigationService.Navigate(pg);
            
        }

        /**
         * <summary>Button to go back to the detailscherm</summary>
         */
        private void BackButton(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        
        /**
         * <summary>Show the overzicht screen</summary>
         */
        private void OverviewScreen(object sender, RoutedEventArgs e)
        {
            // check if not already on this screen
            if (screen == 0) return;
            screen = 0;
            // create and goto page
            StudentDetailsPage pg = new(selectedStudent);
            pg.AddStudentInfo();
            DetailFrame.NavigationService.Navigate(pg);
        }

        /**
         * <summary>Plan a meeting</summary>
         */
        private void CreateMeetingScreen(object sender, RoutedEventArgs e)
        {
            //open screen to plan a meeting
            Inplannen inplannen = new(selectedStudent);
            inplannen.studentnr = studentnr;
            inplannen.ShowDialog();
        }

        /**
         * <summary>Show the problems screen</summary>
         */
        private void StudentProblemsOverviewScreen(object sender, RoutedEventArgs e)
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
        private void StudentLearningGoalsList(object sender, RoutedEventArgs e)
        {
            // check if not already on this screen
            if (screen == 2) return;
            screen = 2;
            // create and goto page
            ShowStudentLeerdoelenTable StudentLearningGoalTable = new(selectedStudent);
            DetailFrame.NavigationService.Navigate(StudentLearningGoalTable);
        }


        private void Detailscreen_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            _scaleEngine.OnWindowSizeChange(sender, e);
        }
    }
}
 