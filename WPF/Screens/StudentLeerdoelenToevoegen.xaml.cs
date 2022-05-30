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
        Student selectedstudent;
        public StudentLeerdoelenToevoegen(Student st)
        {
            InitializeComponent();
            selectedstudent = st;
        }
        
        /// <summary>
        /// Submit the <see cref="leerdoel"/> Binded to <see cref="Student"/>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new StudentBeleidContext())
            {
                Leerdoel leerdoel = new Leerdoel
                {
                    Beschrijving = this.leerdoel.Text,
                    StudentId = selectedstudent.Id,
                };
                context.Leerdoelen.Add(leerdoel);
                context.SaveChanges();
            }



        }

        /*        /// <summary>
                /// Search the student and place them in the combobox
                /// </summary>
                /// <param name="sender"></param>
                /// <param name="e"></param>
                private void Search_OnClick(object sender, RoutedEventArgs e)
                {
                    List<Student> student = SmartSearch.SmartSearchStudent(Student.Text, App.context);
                    Studentselection.ItemsSource =
                        student.Select(s => new KeyValuePair<String,Student>($"{s.Studentnummer}, {s.Voornaam}, {s.Tussenvoegsel}, {s.Achternaam}",s)).ToList();
                    Studentselection.SelectedIndex= 0;
                }*/
    }
}
