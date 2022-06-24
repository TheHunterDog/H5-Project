#region

using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Database.Model;

#endregion

namespace WPF.Screens
{
    /// <summary>
    /// Interaction logic for promoteDemoteTeacher.xaml
    /// </summary>
    public partial class promoteDemoteTeacher : Window
    {
        public List<StudentSupervisor> coaches;
        public List<Teacher> teachers;
        public Teacher SelectedTeacher;
        public StudentSupervisor SelectedSber;
        public bool promotion;
        public promoteDemoteTeacher(bool promote, StudentSupervisor sber = null, Teacher teacher = null)
        {
            
            InitializeComponent();
            using (var context = new DatabaseContext())
            {
                //create a teacher and coaches list
                coaches = context.StudentSupervisor.ToList();
                teachers = context.Teacher.ToList();
            }
            SelectedSber = sber;
            SelectedTeacher = teacher;
            promotion = promote;
            setHeader();
        }
        /**
         * <summary>Submites the Sber or Teacher to the database when submit button is pressed</summary>
         */
        private void SubmitBtn(object sender, RoutedEventArgs e)
        {
            // check to promote or demote
            if (promotion)
            {
                using (var context = new DatabaseContext())
                {
                    // create new sber
                    StudentSupervisor studentSupervisor = new StudentSupervisor
                    {
                        Name = SelectedTeacher.Name,
                        TeacherCode = SelectedTeacher.DocentCode,
                        Password = SelectedTeacher.Password,
                        Username = SelectedTeacher.Username
                    };
                    // add sber to database and save
                    context.StudentSupervisor.Add(studentSupervisor);
                    context.SaveChanges();
                }
            }
            else
            {
                using (var context = new DatabaseContext())
                {
                    // create new teacher
                    Teacher teacher = new Teacher
                    {
                        Name = SelectedSber.Name,
                        DocentCode = SelectedSber.TeacherCode,
                        Password = SelectedSber.Password,
                        Username = SelectedSber.Username
                    };
                    // add teacher to database and save
                    context.Teacher.Add(teacher);
                    context.SaveChanges();
                }
            }
            Close();
        }
        /**
         * <summary>Closes current window</summary>
         */
        private void CancelBtn(object sender, RoutedEventArgs e)
        {
            Close();
        }
        /**
         * <summary>Sets the header with the correct name</summary>
         */
        void setHeader()
        {
            if (promotion)
                // set header to display teacher name
                Confirmation.Content = $"Weet u zeker dat u: {SelectedTeacher.Name} wilt veranderen naar SBer?";
            else
                // set header to display sber name
                Confirmation.Content = $"Weet u zeker dat u: {SelectedSber.Name} wilt veranderen naar docent?";
        }

    }
}
