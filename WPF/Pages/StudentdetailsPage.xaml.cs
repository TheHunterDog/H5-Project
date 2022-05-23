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
using System.Windows.Navigation;
using System.Windows.Shapes;
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

        public void addStudentInfo()
        {
            using (var context = new StudentBeleidContext())
            {
                // fill the labels with the student's info
                information.Content = $"informatie student: {studentnr}";
                naam.Content = $"Naam: {selectedStudent.Voornaam}{(" " + selectedStudent.Tussenvoegsel).TrimEnd()} {selectedStudent.Achternaam}";
                studentnum.Content = $"Studentnummer: {selectedStudent.Studentnummer}";
                klas.Content = $"Klas: {selectedStudent.Klasscode}";
                SBer.Content = $"studentbegeleider: {context.StudentBegeleiders.Where(x => x.Id == selectedStudent.StudentbegeleiderId).First().Naam}";
                isMessagePlanned.Content = meetingIsPlanned(selectedStudent.Studentnummer);
                lastMeeting.Content = $"Laatste gesprek was op: {context.StudentBegeleiderGesprekken.Where(x => x.GesprekDatum < DateTime.Now).First().GesprekDatum}";
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
    }
}
