using System.Windows;
using Database.Model;


namespace WPF
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
            using (var context = new StudentBeleidContext())
            {
                Leerdoel leerdoel = new Leerdoel
                {
                    Beschrijving = this.leerdoel.Text,
                    StudentId = selectedstudent.Id,
                };
                context.Leerdoelen.Add(leerdoel);
                context.SaveChanges();
            }
            Close();
        }
    }
}
