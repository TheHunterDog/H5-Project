﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Net.Mail;
using System.Windows.Controls;
using Database.Model;
using WPF.Util;


namespace WPF
{
    /// <summary>
    /// Interaction logic for StudentLeerdoelenToevoegen.xaml
    /// </summary>
    public partial class StudentLeerdoelenToevoegen : Window
    {
        public StudentLeerdoelenToevoegen()
        {
            InitializeComponent();
        }
        
        /**
         * <summary>button click logic</summary> 
         */
        private void Submit_Click(object sender, RoutedEventArgs e)
        {

            
            List<Student> student = SmartSearch.SmartSearchStudent(Student.Text, App.context);
            KeyValuePair<String,Student> s = (KeyValuePair<String,Student>) Studentselection.SelectedItem;
            Leerdoel leerdoel = new Leerdoel
                {
                    Beschrijving = this.leerdoel.Text,
                    StudentId = s.Value.Id,
                    Student = s.Value
                };

                App.context.Leerdoelen.Add(leerdoel);
                App.context.SaveChanges();
                Close();
        }

        private void Search_OnClick(object sender, RoutedEventArgs e)
        {
            List<Student> student = SmartSearch.SmartSearchStudent(Student.Text, App.context);
            Studentselection.ItemsSource =
                student.Select(s => new KeyValuePair<String,Student>($"{s.Studentnummer}, {s.Voornaam}, {s.Tussenvoegsel}, {s.Achternaam}",s)).ToList();
            Studentselection.SelectedIndex= 0;
        }
    }
}