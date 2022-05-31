﻿using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using Database.Model;
using WPF.Screens;

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
        private IAuthenticatable? user;

        public MainWindow()
        {
            InitializeComponent();
        }
        public MainWindow(IAuthenticatable? user)
        {
            InitializeComponent();
            this.user = user;
             UsernameBtn.Content = user.Username;
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

        public void Logout()
        {
            if (user != null)
            {
                user = null;
                this.Close();
                Login l = new Login();
                l.Show();
                this.Close();
            }
        }
    }
}