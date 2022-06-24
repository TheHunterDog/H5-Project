#region

using System.Windows;
using Database.Model;

#endregion

namespace WPF.Screens
{
    /// <summary>
    /// Interaction logic for StudentLeerdoelenToevoegen.xaml
    /// </summary>
    public partial class StudentLeerdoelenToevoegen : Window
    {
        Student selectedstudent;
        public StudentLeerdoelenToevoegen(Student st)
        {
            InitializeComponent();
            selectedstudent = st;
        }
        
        /// <summary>
        /// Submit the <see cref="leerdoel"/> Binded to <see cref="Student"/>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new DatabaseContext())
            {
                LearningGoal learningGoal = new LearningGoal
                {
                    Description = this.leerdoel.Text,
                    StudentId = selectedstudent.Id,
                };
                context.LearningGoals.Add(learningGoal);
                context.SaveChanges();
            }
            Close();
        }
    }
}
