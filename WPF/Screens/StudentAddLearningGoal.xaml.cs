#region

using System.Windows;
using Database.Model;

#endregion

namespace WPF.Screens
{
    /// <summary>
    /// Interaction logic for StudentLeerdoelenToevoegen.xaml
    /// </summary>
    public partial class StudentAddLearningGoal : Window
    {
        Student selectedstudent;
        public StudentAddLearningGoal(Student st)
        {
            InitializeComponent();
            selectedstudent = st;
        }
        
        /// <summary>
        /// Submit the <see cref="LearningGoal"/> Binded to <see cref="Student"/>
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
