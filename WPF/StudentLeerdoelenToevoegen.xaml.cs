using System;
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
            Studentselection.DisplayMemberPath = "Key";
            Studentselection.SelectedValuePath = "Value";
        }
        
        /// <summary>
        /// Submit the <see cref="leerdoel"/> Binded to <see cref="Student"/>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Submit_Click(object sender, RoutedEventArgs e)
        {
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

        /// <summary>
        /// Search the student and place them in the combobox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Search_OnClick(object sender, RoutedEventArgs e)
        {
            List<Student> student = SmartSearch.SmartSearchStudent(Student.Text, App.context);
            Studentselection.ItemsSource =
                student.Select(s => new KeyValuePair<String,Student>($"{s.Studentnummer}, {s.Voornaam}, {(s.Tussenvoegsel != null ? s.Tussenvoegsel + ", ": "")} {s.Achternaam}",s)).ToList();
            Studentselection.SelectedIndex= 0;
        }
    }
}
