﻿using System;
using System.Windows;

namespace WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        // Keep track of open page
        // 0 is studentList
        // 1 is anotherPage
        private int _screen = -1;
        
        public MainWindow()
        {
            InitializeComponent();
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
            MainFrame.Navigate(new Uri("Pages/AnotherPage.xaml", UriKind.RelativeOrAbsolute));
            MainFrame.NavigationService.RemoveBackEntry();
            _screen = 1;
        }

        private void Leerdoelen_OnClick(object sender, RoutedEventArgs e)
        {
            new ShowStudentLeerdoelenTable().Show();
        }

        private void LeerdoelenToevoegen_OnClick(object sender, RoutedEventArgs e)
        {
            new StudentLeerdoelenToevoegen().Show();
        }
    }
}