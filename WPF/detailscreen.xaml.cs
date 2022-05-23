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

namespace WPF
{
    /// <summary>
    /// Interaction logic for detailscreen.xaml
    /// </summary>
    public partial class Detailscreen : Window
    {
        public string studentnr;
        Student selectedStudent;
        public Detailscreen()
        {
            InitializeComponent();
        }

        public void addStudentInfo()
        {
            using (StudentBeleidContext context = new StudentBeleidContext())
            {
                // find the students
                selectedStudent = context.Students.Where(x => x.Studentnummer == studentnr).First();
                // fill the labels with the student's info
                information.Content = $"informatie student: {studentnr}";
                naam.Content = $"Naam: {selectedStudent.Voornaam}{(" " + selectedStudent.Tussenvoegsel).TrimEnd()} {selectedStudent.Achternaam}";
                studentnum.Content = $"Studentnummer: {selectedStudent.Studentnummer}";
                klas.Content = $"Klas: {selectedStudent.Klasscode}";
                SBer.Content = $"studentbegeleider: {context.StudentBegeleiders.Where(x => x.Id == selectedStudent.StudentbegeleiderId).First().Naam}";
                isMessagePlanned.Content = meetingIsPlanned(selectedStudent.Studentnummer);
            }
            
        }

        /**<summary>function to check if there is a meeting planned</summary> */
        string meetingIsPlanned(string studentid)
        {
            string message = "";
            using (StudentBeleidContext context = new StudentBeleidContext())
            {
                if (context.StudentBegeleiderGesprekken.Where(x => x.StudentId == selectedStudent.Id && x.GesprekDatum >= DateTime.Now).FirstOrDefault() == null)
                {
                    message = "op dit moment is er geen gesprek ingepland";
                }
                else
                {
                    message = $"er is een gesprek geplanned voor: {context.StudentBegeleiderGesprekken.Where(x => x.StudentId == selectedStudent.Id).First().GesprekDatum}";
                }
                return message;
            }
        }

        private void backButton(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void planMeeting(object sender, RoutedEventArgs e)
        {
            Inplannen inplannen = new Inplannen();
            inplannen.studentnr = studentnr;
            inplannen.ShowDialog();
        }

        private void meetingList(object sender, RoutedEventArgs e)
        {

        }
    }
}
