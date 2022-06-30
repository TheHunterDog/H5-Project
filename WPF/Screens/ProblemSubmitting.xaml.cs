#region

using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Database;
using Database.Model;

#endregion

namespace WPF.Screens
{
    /// <summary>
    /// Interaction logic for ProblemSubmitting.xaml
    /// </summary>
    public partial class ProblemSubmitting : Window
    {
        private StudentProblem _stagedProblem;

        /**
         * <summary>Initialize problem on load window</summary>
         */
        public ProblemSubmitting(int studentId = -1, int teacherId = -1)
        {
            InitializeComponent();

            // create new studentproblem
            _stagedProblem = CreateStudentProblem(studentId, teacherId);
            if (_stagedProblem == null) CloseWindow();

            // set title
            using (var context = new DatabaseContext())
            {
                Title.Content = "Invoeren probleem met " + GetStudentNumberFromStudentId(_stagedProblem.StudentId);
            }
        }

        /**
         * <summary>Submitting problem</summary>
         */
        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (_stagedProblem == null || _stagedProblem.Description == "") return;

            // Trace.WriteLine(_stagedProblem);

            AddProblemToDatabase(_stagedProblem);

            CloseWindow();
        }

        /**
         * <summary>Updating problem description</summary>
         */
        private void OnProblemDescriptionChanged(object sender, TextChangedEventArgs e)
        {
            // do nothing if the staged problem is empty
            if (_stagedProblem == null) return;
            //change staged problem description to the selected description
            _stagedProblem.Description = Problem.Text;
        }

        /**
         * <summary>Updating problem priority</summary>
         */
        private void OnPrioritySelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // do nothing if the staged problem is empty
            if (_stagedProblem == null) return;

            //change staged problems priority to the selected priority
            string? v = ((ComboBoxItem)Priority.SelectedItem).Tag.ToString();
            string tag = v;
            _stagedProblem.Priority = PriorityTextToInt(tag);
        }

        /**
         * <summary>converting dropdown item tag to int value</summary>
         */
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

        /**
         * <summary>close problem submitting window</summary>
         */
        private void CloseWindow()
        {
            Close();
        }

        /**
         * <summary>creates a blank student problem</summary>
         */
        public StudentProblem CreateStudentProblem(int studentId, int teacherId)
        {
            // initialize new problem
            StudentProblem studentProblem = new StudentProblem();

            // set student- and teacher id, first if no id is given, closes window if invalid id's are given
            using (var context = new DatabaseContext())
            {
                if (studentId == -1) studentId = context.Student.First().Id;
                else if (context.Student.Find(studentId) == null) return null;
                if (teacherId == -1) teacherId = context.Teacher.First().Id;
                else if (context.Teacher.Find(teacherId) == null) return null;
                studentProblem.StudentId = studentId;
                studentProblem.TeacherId = teacherId;
                studentProblem.Description = "";
                studentProblem.Priority = 0;
            }

            return studentProblem;
        }
        /**
         * <summary>gets the student from the student Id</summary>
         */
        public string GetStudentNumberFromStudentId(int studentId)
        {
            using (var context = new DatabaseContext())
            {
                // find the student with the Id
                Student student = context.Student.Find(studentId);
                // if there is no student do nothing else return the student
                if (student != null)
                {
                    return context.Student.Find(studentId).StudentNumber;
                }
                else return "";
            }
        }

        /**
         * <summary>add problem to database</summary>
         */
        public void AddProblemToDatabase(StudentProblem studentProblem)
        {
            using (var context = new DatabaseContext())
            {
                // if not a  problem, do nothing
                if (studentProblem.Description.Equals("")) return;
                if (context.Student.Find(studentProblem.StudentId) == null) return;
                if (context.Teacher.Find(studentProblem.TeacherId) == null) return;

                context.StudentProblem.Add(studentProblem);
                // save changes to database
                context.SaveChanges();
            }
        }
    }
}
