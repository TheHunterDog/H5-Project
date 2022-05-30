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
    /// Interaction logic for AddSubject.xaml
    /// </summary>
    public partial class AddSubject : Window
    {
        private Subject stagedSubject;
        // initialize problem on load window
        public AddSubject()
        {
            InitializeComponent();

            stagedSubject = CreateSubject();
        }

        public Subject CreateSubject()
        {
            // initialize new problem
            Subject subject = new Subject();

            return subject;
        }

        public bool FillSubject(Subject subject)
        {
            // check if subject is null, if so creates a new one
            if (subject == null) subject = CreateSubject();

            // checks if value is valid, if not return false
            if (Name.Text != null && Name.Text != "") subject.Name = Name.Text;
            else return false;
/*            // checks if value is valid, if not return false
            if (Code.Text != null && Code.Text != "") subject.Code = Code.Text;
            else return false;*/
            // checks if value is valid, if not return false
            if (Description.Text != null && Description.Text != "") subject.Description = Description.Text;
            else return false;
/*            // checks if value is valid, if not return false
            if (EC.Text != null && EC.Text != "")
            {
                try
                {
                    int value = Int32.Parse(EC.Text);
                    subject.Ec = value;
                }
                catch (FormatException ex)
                {
                    return false;
                }
            }
            else return false;
            // checks if value is valid, if not return false
            if (Lessons.Text != null && Lessons.Text != "")
            {
                try
                {
                    int value = Int32.Parse(Lessons.Text);
                    subject.Lessen = value;
                }
                catch (FormatException ex)
                {
                    return false;
                }
            }
            else return false;*/

            return true;
        }

        public void AddSubjectToDatabase(Subject subject)
        {
            using (var context = App.context)
            {
                context.Subjects.Add(subject);
                context.SaveChanges();
            }
        }

        // submitting problem
        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            // checks if filling subject parameters was successfull, if not returns without saving or closing window
            if (FillSubject(stagedSubject))
            {
                Trace.WriteLine(stagedSubject);
                // save subject to database
                AddSubjectToDatabase(stagedSubject);
            }
            else
            {
                Trace.WriteLine(stagedSubject);
                return;
            }
            // close window
            CloseWindow();
        }

        // close problem submitting window
        private void CloseWindow()
        {
            Close();
        }
    }
/*
    public class Subject
    {
        public string Naam = "";
        public string Code = "";
        public string Description = "";
        public int Ec = 0;
        public int Lessen = 0;

        public override string ToString()
        {
            return $"{Naam}, {Code}, {Description}, {Ec}, {Lessen}";
        }
    }*/

}
