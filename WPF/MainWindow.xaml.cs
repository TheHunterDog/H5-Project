﻿using System;
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
            
        }

        private void FrameworkElement_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            Console.WriteLine("HELLO WORLD!");
        }
    }
}
