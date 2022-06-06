using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using Database.Model;

namespace WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {

        // Keep track of open page
        // 0 is studentList
        // 1 is Meeting List
        // 2 is Meldingen List
        // 3 is manage screen
        private int _screen = -1;
        private IAuthenticatable? user;

        public MainWindow()
        {
            InitializeComponent();
        }
        public MainWindow(IAuthenticatable? user)
        {
            InitializeComponent();
            this.user = user;
             UsernameBtn.Header = user.Username;
        }

        private void OpenStudentListView(object sender, RoutedEventArgs e)
        {
            if (_screen == 0) return;
            MainFrame.Navigate(new Uri("Pages/StudentTable.xaml", UriKind.RelativeOrAbsolute));
            MainFrame.NavigationService.RemoveBackEntry();
            _screen = 0;
        }

        private void OpenMeetingListView(object sender, RoutedEventArgs e)
        {
            if (_screen == 1) return;
            MainFrame.Navigate(new Uri("Pages/MeetingList.xaml", UriKind.RelativeOrAbsolute));
            MainFrame.NavigationService.RemoveBackEntry();
            _screen = 1;
        }

        private void OpenMeldingListView(object sender, RoutedEventArgs e)
        {
            if (_screen == 2) return;
            MainFrame.Navigate(new Uri("Pages/ProblemsList.xaml", UriKind.RelativeOrAbsolute));
            MainFrame.NavigationService.RemoveBackEntry();
            _screen = 2;
        }

        private void ManageCoachesBtn(object sender, RoutedEventArgs e)
        {
            if (_screen == 3) return;
            MainFrame.Navigate(new Uri("Pages/ManageScreen.xaml", UriKind.RelativeOrAbsolute));
            MainFrame.NavigationService.RemoveBackEntry();
            _screen = 3;
        }



        
    }
}