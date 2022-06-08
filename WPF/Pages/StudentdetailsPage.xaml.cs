using System;
using System.Linq;
using System.Windows.Controls;
using Database.Model;

namespace WPF.Pages
{
    /// <summary>
    /// Interaction logic for StudentdetailsPage.xaml
    /// </summary>
    public partial class StudentdetailsPage : Page
    {
        public string studentnr;
        Student selectedStudent;
        public StudentdetailsPage(Student st)
        {
            InitializeComponent();
            selectedStudent = st;
        }

        /**
         * <summary>Add the info of the selected student to the page</summary>
         */
        public void addStudentInfo()
        {
            using (var context = new StudentBeleidContext())
            {
                // fill the labels with the student's info
                information.Content = $"Informatie student: {selectedStudent.Studentnummer}";
                naam.Content = $"Naam: {selectedStudent.Voornaam}{(" " + selectedStudent.Tussenvoegsel).TrimEnd()} {selectedStudent.Achternaam}";
                studentnum.Content = $"Studentnummer: {selectedStudent.Studentnummer}";
                klas.Content = $"Klas: {selectedStudent.Klasscode}";
                SBer.Content = $"Studentbegeleider: {context.StudentBegeleiders.Where(x => x.Id == selectedStudent.StudentbegeleiderId).First().Naam}";
                isMessagePlanned.Content = meetingIsPlanned();
                lastMeeting.Content = lastMeetingCheck();
            }

        }

        /**<summary>Function to check if there is a meeting planned</summary> */
        string meetingIsPlanned()
        {
            string message = "";
            using (StudentBeleidContext context = new StudentBeleidContext())
            {
                var gesprek = context.StudentBegeleiderGesprekken.Where(x => x.StudentId == selectedStudent.Id && x.GesprekDatum >= DateTime.Now).FirstOrDefault();
                // check if there is a meeting planned
                if (gesprek == null)
                    message = "Op dit moment is er geen gesprek ingepland";
                else
                    message = $"Er is een gesprek geplanned voor: {gesprek.GesprekDatum}";
                return message;
            }
        }

        /**
         * <summary>Check when the last meeting was</summary>
         */
        string lastMeetingCheck()
        {
            string message = "";
            using (var context = new StudentBeleidContext())
            {
                var gesprek = context.StudentBegeleiderGesprekken.Where(x => x.GesprekDatum < DateTime.Now && x.StudentId == selectedStudent.Id).FirstOrDefault();
                // check if there was a meeting
                if (gesprek != null && gesprek.Voltooid )
                    message = $"Laatste gesprek was op: {gesprek.GesprekDatum}";
                else
                    message = "Er is nog geen gesprek geweest";
                return message;
            }
        }
    }
}
