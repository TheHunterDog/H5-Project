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
using Database.Model;
using System.Diagnostics;

namespace WPF
{
    /// <summary>
    /// Interaction logic for ProblemSubmitting.xaml
    /// </summary>
    public partial class ProblemSubmitting : Window
    {
        private StudentProblem _stagedProblem;

        // initialize problem on load window
        public ProblemSubmitting(int studentId = -1, int teacherId = -1)
        {
            InitializeComponent();

            PrintAllProblems();

            _stagedProblem = CreateStudentProblem(studentId, teacherId);
            if (_stagedProblem == null) CloseWindow();

            // set title
            using (var context = new StudentBeleidContext())
            {
                Title.Content = "Invoeren probleem met " + GetStudentNumberFromStudentId(_stagedProblem.StudentId);
            }
        }

        // submitting problem
        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (_stagedProblem == null || _stagedProblem.Description == "") return;

            // Trace.WriteLine(_stagedProblem);

            AddProblemToDatabase(_stagedProblem);

            CloseWindow();
        }

        // updating problem description
        private void OnProblemDescriptionChanged(object sender, TextChangedEventArgs e)
        {
            if (_stagedProblem == null) return;

            _stagedProblem.Description = Problem.Text;
        }

        // updating problem priority
        private void OnPrioritySelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_stagedProblem == null) return;

            string? v = ((ComboBoxItem)Priority.SelectedItem).Tag.ToString();
            string tag = v;
            _stagedProblem.Priority = PriorityTextToInt(tag);
        }

        // converting dropdown item tag to int value
        public int PriorityTextToInt(string text)
        {
            if (text.Equals("0"))
            {
                return 0;
            }
            else if (text.Equals("1"))
            {
                return 1;
            }
            else
            {
                return 2;
            }
        }

        // close problem submitting window
        private void CloseWindow()
        {
            Close();
        }

        public StudentProblem CreateStudentProblem(int studentId, int teacherId)
        {
            // initialize new problem
            StudentProblem studentProblem = new StudentProblem();

            // set student- and teacher id, first if no id is given, closes window if invalid id's are given
            using (var context = new StudentBeleidContext())
            {
                if (studentId == -1) studentId = context.Students.First().Id;
                else if (context.Students.Find(studentId) == null) return null;
                if (teacherId == -1) teacherId = context.Teachers.First().Id;
                else if (context.Teachers.Find(teacherId) == null) return null;
                studentProblem.StudentId = studentId;
                studentProblem.TeacherId = teacherId;
                studentProblem.Description = "";
                studentProblem.Priority = 0;
            }

            return studentProblem;
        }

        public string GetStudentNumberFromStudentId(int studentId)
        {
            using (var context = new StudentBeleidContext())
            {
                Student student = context.Students.Find(studentId);
                if (student != null)
                {
                    return context.Students.Find(studentId).Studentnummer;
                }
                else return "";
            }
        }

        public void AddProblemToDatabase(StudentProblem studentProblem)
        {
            using (var context = new StudentBeleidContext())
            {
                if (studentProblem.Description.Equals("")) return;
                if (context.Students.Find(studentProblem.StudentId) == null) return;
                if (context.Teachers.Find(studentProblem.TeacherId) == null) return;

                context.StudentProblems.Add(studentProblem);
                // save changes to database
                context.SaveChanges();
            }
        }

        private void PrintAllProblems()
        {
            Trace.WriteLine("\n All Problems in database:");
            try
            {
                using (var context = new StudentBeleidContext())
                {
                    // obtain students from database
                    List<StudentProblem> problems = context.StudentProblems.ToList();
                    for (int i = 0; i < problems.Count; i++)
                    {
                        // get student from list
                        StudentProblem problem = problems.ElementAt(i);
                        // print student data
                        Trace.WriteLine(problem);
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }


        }
    }
/*
    public class Problem
    {
        public int StudentID;
        public string Description = "Description";
        public int Priority = 0;
        public int TeacherID;

        public override string ToString()
        {
            return $"{StudentID}, {Description}, {Priority}, {TeacherID}";
        }
    }*/

}
