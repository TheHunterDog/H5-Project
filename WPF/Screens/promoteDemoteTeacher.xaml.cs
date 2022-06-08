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
        public List<StudentBegeleider> coaches;
        public List<Teacher> teachers;
        public Teacher SelectedTeacher;
        public StudentBegeleider SelectedSber;
        public bool promotion;
        public promoteDemoteTeacher(bool promote, StudentBegeleider sber = null, Teacher teacher = null)
        {
            
            InitializeComponent();
            using (var context = new StudentBeleidContext())
            {
                //create a teacher and coaches list
                coaches = context.StudentBegeleiders.ToList();
                teachers = context.Teachers.ToList();
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
                using (var context = new StudentBeleidContext())
                {
                    // create new sber
                    StudentBegeleider studentBegeleider = new StudentBegeleider
                    {
                        Naam = SelectedTeacher.Name,
                        Docentcode = SelectedTeacher.DocentCode,
                        Password = SelectedTeacher.Password,
                        Username = SelectedTeacher.Username
                    };
                    // add sber to database and save
                    context.StudentBegeleiders.Add(studentBegeleider);
                    context.SaveChanges();
                }
            }
            else
            {
                using (var context = new StudentBeleidContext())
                {
                    // create new teacher
                    Teacher teacher = new Teacher
                    {
                        Name = SelectedSber.Naam,
                        DocentCode = SelectedSber.Docentcode,
                        Password = SelectedSber.Password,
                        Username = SelectedSber.Username
                    };
                    // add teacher to database and save
                    context.Teachers.Add(teacher);
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
                Confirmation.Content = $"Weet u zeker dat u: {SelectedSber.Naam} wilt veranderen naar docent?";
        }

    }
}
