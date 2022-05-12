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
        private Problem _problem;

        // initialize problem on load window
        public ProblemSubmitting(int studentId = -1, int teacherId = -1)
        {
            InitializeComponent();

            // initialize new problem
            _problem = new Problem();

            // set student- and teacher id, first if no id is given, closes window if invalid id's are given
            using (var context = new StudentBeleidContext())
            {
                if (studentId == -1) studentId = context.Students.First().Id;
                else if (context.Students.Find(studentId) == null) CloseWindow();
                if (teacherId == -1) teacherId = context.StudentBegeleiders.First().Id;
                else if (context.StudentBegeleiders.Find(teacherId) == null) CloseWindow();
                _problem.StudentID = studentId;
                _problem.TeacherID = teacherId;
            }

        }

        // submitting problem
        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (_problem == null || Problem.Text == null) return;

            Trace.WriteLine(_problem);
            CloseWindow();
        }

        // updating problem description
        private void OnProblemDescriptionChanged(object sender, TextChangedEventArgs e)
        {
            if (_problem == null) return;

            _problem.Description = Problem.Text;
        }

        // updating problem priority
        private void OnPrioritySelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_problem == null) return;

            string? v = ((ComboBoxItem)Priority.SelectedItem).Tag.ToString();
            string tag = v;
            _problem.Priority = PriorityTextToInt(tag);
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
    }

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
    }

}
