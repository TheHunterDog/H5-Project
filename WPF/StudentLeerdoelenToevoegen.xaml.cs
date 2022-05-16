using System;
using System.Linq;
using System.Windows;
using System.Net.Mail;
using Database.Model;


namespace WPF
{
    /// <summary>
    /// Interaction logic for StudentLeerdoelenToevoegen.xaml
    /// </summary>
    public partial class StudentLeerdoelenToevoegen : Window
    {
        public StudentLeerdoelenToevoegen()
        {
            InitializeComponent();
        }
        
        /**
         * <summary>button click logic</summary> 
         */
        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            
            using (var context = new StudentBeleidContext())
            {
                Leerdoel leerdoel = new Leerdoel
                {
                    Beschrijving = this.leerdoel.Text,
                    StudentId = context.Students.First().Id,
                    Student = context.Students.First()
                };

                context.Leerdoelen.Add(leerdoel);
                context.SaveChanges();
            }
            Close();
        }
    }
}
