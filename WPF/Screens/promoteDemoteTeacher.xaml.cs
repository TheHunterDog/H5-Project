using Database.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
                coaches = context.StudentBegeleiders.ToList();
                teachers = context.Teachers.ToList();
            }
            SelectedSber = sber;
            SelectedTeacher = teacher;
            promotion = promote;
            setHeader();
        }
        private void SubmitBtn(object sender, RoutedEventArgs e)
        {
            if (promotion)
            {
                using (var context = new StudentBeleidContext())
                {
                    StudentBegeleider studentBegeleider = new StudentBegeleider
                    {
                        Naam = SelectedTeacher.Name,
                        Docentcode = SelectedTeacher.DocentCode,
                        Password = SelectedTeacher.Password,
                        Username = SelectedTeacher.Username
                    };

                    context.StudentBegeleiders.Add(studentBegeleider);
                    context.SaveChanges();
                }
            }
            else
            {
                using (var context = new StudentBeleidContext())
                {
                    Teacher teacher = new Teacher
                    {
                        Name = SelectedSber.Naam,
                        DocentCode = SelectedSber.Docentcode,
                        Password = SelectedSber.Password,
                        Username = SelectedSber.Username
                    };
                    context.Teachers.Add(teacher);
                    context.StudentBegeleiders.Remove(SelectedSber);
                    context.SaveChanges();
                }
            }
            Close();
        }
        private void CancelBtn(object sender, RoutedEventArgs e)
        {
            Close();
        }
        void setHeader()
        {
            if (promotion)
                Confirmation.Content = $"Weet u zeker dat u: {SelectedTeacher.Name} wilt veranderen naar SBer?";
            else
                Confirmation.Content = $"Weet u zeker dat u: {SelectedSber.Naam} wilt veranderen naar docent?";
        }

    }
}
