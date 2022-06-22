#region

using System.Windows;
using Database.Model;

#endregion

namespace WPF.Screens
{
    /** 
     * <summary>Interaction logic for StudentLeerdoelenToevoegen.xaml </summary>
     */
    public partial class SBerToevoegen : Window
    {
        public SBerToevoegen()
        {
            InitializeComponent();
        }

        /**
         * <summary>Submites the Sber or Teacher to the database when submit button is pressed</summary>
         */
        private void SubmitBtn(object sender, RoutedEventArgs e)
        {
            // if name or docentcode is empty, do nothing
            if (name.Text == "") return;
            if (Docentcode.Text == "") return;
            using (var context = new StudentBeleidContext())
            {
                // if it is an sber
                if (IsSber.IsChecked == true)
                {
                    //create new sber
                    StudentSupervisor supervisor = new StudentSupervisor
                    {
                        Name = name.Text,
                        TeacherCode = Docentcode.Text,
                        Username = name.Text,
                        Password = "geheim lol"
                    };
                    context.StudentSupervisors.Add(supervisor);
                }
                else
                {
                    // create new teacher
                    Teacher teacher = new Teacher
                    {
                        Name = name.Text,
                        DocentCode = Docentcode.Text,
                        Username = name.Text,
                        Password = "geheim lol"
                    };
                    context.Teachers.Add(teacher);
                }
                context.SaveChanges();
            }
            // close the window
            Close();
        }
    }
}
