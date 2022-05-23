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

            // create staged problem
            _stagedProblem = CreateStudentProblem(studentId, teacherId);
            if (_stagedProblem == null) CloseWindow();

            // set window title
            using (var context = new StudentBeleidContext())
            {
                Title.Content = "Invoeren probleem met " + GetStudentNumberFromStudentId(_stagedProblem.StudentId);
            }

            Trace.WriteLine(_stagedProblem);
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            // close window
            CloseWindow();
        }

        // submitting problem
        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            // check
            if (_stagedProblem == null || _stagedProblem.Description == "") return;

            // add problem to database
            AddProblemToDatabase(_stagedProblem);

            // close window
            CloseWindow();
        }

        // updating problem description
        private void OnProblemDescriptionChanged(object sender, TextChangedEventArgs e)
        {
            // check
            if (_stagedProblem == null) return;

            // set description to empty string
            _stagedProblem.Description = Problem.Text;
        }

        // updating problem priority
        private void OnPrioritySelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // check
            if (_stagedProblem == null) return;

            // create string from tag
            string? v = ((ComboBoxItem)Priority.SelectedItem).Tag.ToString();
            string tag = v;
            // set priority to int from string
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

        public Model.StudentProblem CreateStudentProblem(int studentId, int teacherId)
        {
            // initialize new problem
            Model.StudentProblem studentProblem = new Model.StudentProblem();

            // set student- and teacher id, first if no id is given, closes window if invalid id's are given
            using (var context = new StudentBeleidContext())
            {
                // check for valid student id, if not, set to first
                if (studentId == -1) studentId = context.Students.First().Id;
                // check if there are students, else return null
                else if (context.Students.Find(studentId) == null) return null;
                // check for valid teacher id, if not, set to first
                if (teacherId == -1) teacherId = context.Teachers.First().Id;
                // check if there are teachers, else return null
                else if (context.Teachers.Find(teacherId) == null) return null;
                // initialize problem parameters
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
                // find student
                Student student = context.Students.Find(studentId);
                if (student != null)
                {
                    // return studentnumber
                    return context.Students.Find(studentId).Studentnummer;
                }
                // return empty string
                else return "";
            }
        }

        public void AddProblemToDatabase(Model.StudentProblem studentProblem)
        {
            using (var context = new StudentBeleidContext())
            {
                // check if description is empty
                if (studentProblem.Description.Equals("")) return;
                // check for valid student id
                if (context.Students.Find(studentProblem.StudentId) == null) return;
                // check for valid teacher id
                if (context.Teachers.Find(studentProblem.TeacherId) == null) return;

                // add to database
                context.StudentProblems.Add(studentProblem);
                // save changes to database

                Trace.WriteLine(_stagedProblem);

                //save changes to database
                context.SaveChanges();

                Trace.WriteLine(_stagedProblem);
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
}
