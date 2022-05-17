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
        private Model.StudentProblem _stagedProblem;

        // initialize problem on load window
        public ProblemSubmitting(int studentId = -1, int teacherId = -1)
        {
            InitializeComponent();

            PrintAllProblems();

            // initialize new problem
            _stagedProblem = new Model.StudentProblem();

            // set student- and teacher id, first if no id is given, closes window if invalid id's are given
            using (var context = new StudentBeleidContext())
            {
                if (studentId == -1) studentId = context.Students.First().Id;
                else if (context.Students.Find(studentId) == null) CloseWindow();
                if (teacherId == -1) teacherId = context.Teachers.First().Id;
                else if (context.Teachers.Find(teacherId) == null) CloseWindow();
                _stagedProblem.StudentId = studentId;
                _stagedProblem.TeacherId = teacherId;

                Title.Content = "Invoeren probleem met " + context.Students.Find(studentId).Studentnummer;
            }

        }

        // submitting problem
        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (_stagedProblem == null || Problem.Text == null) return;

            Trace.WriteLine(_stagedProblem);

            AddProblemToDatabase();

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
        private int PriorityTextToInt(string text)
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

        private void AddProblemToDatabase()
        {
            using (var context = new StudentBeleidContext())
            {
                if (_stagedProblem.Description.Equals("")) return;
                if (context.Students.Find(_stagedProblem.StudentId) == null) return;
                if (context.Teachers.Find(_stagedProblem.TeacherId) == null) return;

                context.StudentProblems.Add(_stagedProblem);
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
                    List<Model.StudentProblem> problems = context.StudentProblems.ToList();
                    for (int i = 0; i < problems.Count; i++)
                    {
                        // get student from list
                        Model.StudentProblem problem = problems.ElementAt(i);
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
