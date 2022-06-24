#region

using System;
using System.Linq;
using System.Windows.Controls;
using Database.Model;

#endregion

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
            using (var context = new DatabaseContext())
            {
                // fill the labels with the student's info
                information.Content = $"Informatie student: {selectedStudent.StudentNumber}";
                naam.Content = $"Name: {selectedStudent.FirstName}{(" " + selectedStudent.MiddleName).TrimEnd()} {selectedStudent.LastName}";
                studentnum.Content = $"StudentNumber: {selectedStudent.StudentNumber}";
                klas.Content = $"Klas: {selectedStudent.ClassCode}";
                SBer.Content = $"Supervisor: {context.StudentSupervisor.Where(x => x.Id == selectedStudent.StudentSupervisor).First().Name}";
                isMessagePlanned.Content = meetingIsPlanned();
                lastMeeting.Content = lastMeetingCheck();
            }

        }

        /**<summary>Function to check if there is a meeting planned</summary> */
        string meetingIsPlanned()
        {
            string message = "";
            using (DatabaseContext context = new DatabaseContext())
            {
                var gesprek = context.StudentSupervisorMeeting.Where(x => x.StudentId == selectedStudent.Id && x.MeetingDate >= DateTime.Now).FirstOrDefault();
                // check if there is a meeting planned
                if (gesprek == null)
                    message = "Op dit moment is er geen gesprek ingepland";
                else
                    message = $"Er is een gesprek geplanned voor: {gesprek.MeetingDate}";
                return message;
            }
        }

        /**
         * <summary>Check when the last meeting was</summary>
         */
        string lastMeetingCheck()
        {
            string message = "";
            using (var context = new DatabaseContext())
            {
                var gesprek = context.StudentSupervisorMeeting.Where(x => x.MeetingDate < DateTime.Now && x.StudentId == selectedStudent.Id).FirstOrDefault();
                // check if there was a meeting
                if (gesprek != null && gesprek.Done )
                    message = $"Laatste gesprek was op: {gesprek.MeetingDate}";
                else
                    message = "Er is nog geen gesprek geweest";
                return message;
            }
        }
    }
}
