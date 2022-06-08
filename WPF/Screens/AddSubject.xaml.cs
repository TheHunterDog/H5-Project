using System;
using System.Windows;
using Database.Model;

namespace WPF
{
    /**
     * <summary>Interaction logic for AddSubject.xaml</summary>
     */
    public partial class AddSubject : Window
    {
        private Subject stagedSubject;
        // initialize problem on load window
        public AddSubject()
        {
            InitializeComponent();

            stagedSubject = CreateSubject();
        }

        /**
         * <summary>Create a blank subject</summary>
         */
        public Subject CreateSubject()
        {
            // initialize new problem
            Subject subject = new Subject();

            return subject;
        }

        /**
         * <summary>Fill the subject with the information</summary>
         */
        public bool FillSubject(Subject subject)
        {
            // check if subject is null, if so creates a new one
            if (subject == null) subject = CreateSubject();

            // checks if value is valid, if not return false
            if (Name.Text != null && Name.Text != "") subject.Name = Name.Text;
            else return false;
            // checks if value is valid, if not return false
            if (Code.Text != null && Code.Text != "") subject.SubjectCode = Code.Text;
            else return false;
            // checks if value is valid, if not return false
            if (Description.Text != null && Description.Text != "") subject.Description = Description.Text;
            else return false;
            // checks if value is valid, if not return false
            if (EC.Text != null && EC.Text != "")
            {
                try
                {
                    //int value = Int32.Parse(EC.Text);
                    subject.Ec = EC.Text;
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
                    subject.Lessons = value;
                }
                catch (FormatException ex)
                {
                    return false;
                }
            }
            else return false;

            return true;
        }

        /**
         * <summary>Add the subject to database</summary>
         */
        public void AddSubjectToDatabase(Subject subject)
        {
            using (var context = App.context)
            {
                // add and save the subject
                context.Subjects.Add(subject);
                context.SaveChanges();
            }
        }

        /**
         * <summary>Submit a problem to database</summary>
         */
        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            // checks if filling subject parameters was successfull, if not returns without saving or closing window
            if (FillSubject(stagedSubject))
            {
                // save subject to database
                AddSubjectToDatabase(stagedSubject);
            }
            else
            {
                return;
            }
            // close window
            Close();
        }
    }
}
