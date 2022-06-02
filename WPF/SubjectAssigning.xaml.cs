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
    /// Interaction logic for SubjectAssigning.xaml
    /// </summary>
    public partial class SubjectAssigning : Window
    {
        public List<Student> _stagedStudents;
        public List<Subject> _stagedSubjects;


        // initialize problem on load window
        public SubjectAssigning()
        {
            InitializeComponent();

            _stagedStudents = new List<Student>();
            _stagedSubjects = new List<Subject>();
        }

        // finds students by query
        public void FindStudents(string query)
        {
            // reset student list
            _stagedStudents = new List<Student>();

            // split string by commas
            List<String> entries = query.Split(",").ToList();
            // filter entries
            for (int j = 0; j < entries.Count; j++)
            {
                // remove spaces
                entries[j].Trim();

                if (entries[j].Equals(""))
                {
                    // remove emtpy entries
                    entries.RemoveAt(j);
                    j--;
                }
            }
            // find for each entry
            for (int j = 0; j < entries.Count; j++)
            {
                // find classes, with count
                int count = FindClass(entries[j]);
                // if count is 0, check value as student info
                if (count == 0)
                {
                    // find students
                    FindStudent(entries[j]);
                }
            }
            // remove double found students
            RemoveDoubleStudents();
            // update text on button to be consistent with staged student(s) and subject(s)
            UpdateButtonText();
        }

        // finds all students in a class
        public int FindClass(string className)
        {
            // set string to lowercase, and remove spaces
            className = className.ToLower().Trim();
            // retrieve count of found students before finding more students
            int count = _stagedStudents.Count;
            using (var context = new StudentBeleidContext())
            {
                // add students to list who are in the exact class, or a class that contains the string, if the string is more than 1 letter
                _stagedStudents.AddRange(
                    context.Students.Where(
                        s => (
                        s.Klasscode.ToLower().Equals(className) || 
                        (className.Length > 5 && s.Klasscode.ToLower().Contains(className))
                        ) && !_stagedStudents.Contains(s)
                    ).ToList());
            }
            // return new amount of students found minus old count
            return _stagedStudents.Count - count;
        }

        // finds all students by name or number
        public void FindStudent(string studentName)
        {
            // set string to lowercase
            studentName = studentName.ToLower();
            using (var context = new StudentBeleidContext())
            {
                // split string by commas
                List<String> entries = studentName.Split(" ").ToList();
                // filter entries
                for (int j = 0; j < entries.Count; j++)
                {
                    // remove spaces
                    entries[j].Trim();
                    if (entries[j].Equals(""))
                    {                    
                        // remove emtpy entries
                        entries.RemoveAt(j);
                        j--;
                    }
                }
                // check if the amount of entries is more than 0
                if (entries.Count > 0)
                {
                    // check if there is one entry
                    if (entries.Count == 1)
                    {
                        int number;
                        // check if entry is a number, if so set number to that number
                        bool isNumeric = int.TryParse(entries[0], out number);
                        if (isNumeric)
                        {
                            // add an 's' to the front of the entry
                            entries[0] = "s" + entries[0];
                        }
                        // add students that have the entry as number or name
                        _stagedStudents.AddRange(
                            context.Students.Where(
                                s => (
                                s.Studentnummer.ToLower().Equals(entries[0]) || 
                                s.Voornaam.ToLower().Equals(entries[0]) ||
                                s.Achternaam.ToLower().Equals(entries[0])
                                ) && !_stagedStudents.Contains(s)
                            ).ToList());
                    }
                    // check if there are two entries
                    else if (entries.Count == 2)
                    {
                        // add students that have the entries as first name and last name
                        _stagedStudents.AddRange(
                            context.Students.Where(
                                s => (
                                s.Voornaam.ToLower().Equals(entries[0]) ||
                                s.Voornaam.ToLower().Equals(entries[1])
                                ) && (
                                s.Achternaam.ToLower().Equals(entries[0]) ||
                                s.Achternaam.ToLower().Equals(entries[1])
                                ) && !_stagedStudents.Contains(s)
                            ).ToList());
                    }
                    // check if there are three entries
                    else if (entries.Count == 3)
                    {
                        // add students that have the entries as first name, last name and surname
                        _stagedStudents.AddRange(
                            context.Students.Where(
                                s => (
                                s.Voornaam.ToLower().Equals(entries[0]) ||
                                s.Voornaam.ToLower().Equals(entries[1]) ||
                                s.Voornaam.ToLower().Equals(entries[2])
                                ) && (
                                s.Achternaam.ToLower().Equals(entries[0]) ||
                                s.Achternaam.ToLower().Equals(entries[1]) ||
                                s.Achternaam.ToLower().Equals(entries[2])
                                ) && (
                                s.Tussenvoegsel.ToLower().Equals(entries[0]) ||
                                s.Tussenvoegsel.ToLower().Equals(entries[1]) ||
                                s.Tussenvoegsel.ToLower().Equals(entries[2])
                                ) && !_stagedStudents.Contains(s)
                            ).ToList());
                    }
                }
            }
        }

        // removes double found students
        public void RemoveDoubleStudents()
        {
            // finds unique students by override void ToHashSet in Student, which hashes the id of the student
            _stagedStudents.ToHashSet();
        }

        public void DisplayStudentsResult()
        {
            using (var context = new StudentBeleidContext())
            {
                // check if students are found
                if (_stagedStudents.Count > 0)
                {
                    // display studentcount + "student(en) gevonden"
                    ClassResult.Content = _stagedStudents.Count + " Student" + ((_stagedStudents.Count > 1) ? "en" : "") + " gevonden";
                }
                else
                {
                    // display "Geen student(en) gevonden"
                    ClassResult.Content = "Geen student(en) gevonden";
                }
            }
        }


        public void FindSubjects(string query)
        {
            _stagedSubjects = new List<Subject>();

            List<String> entries = query.Split(",").ToList();
            for (int j = 0; j < entries.Count; j++)
            {
                entries[j].Trim();

                if (entries[j].Equals(""))
                {
                    entries.RemoveAt(j);
                    j--;
                }
            }

            for (int j = 0; j < entries.Count; j++)
            {
                FindSubject(entries[j]);
            }
            // remove double found subjects
            RemoveDoubleSubjects();
            // update text on button to be consistent with staged student(s) and subject(s)
            UpdateButtonText();
        }

        // finds all subjects by name
        public void FindSubject(string subjectName)
        {
            subjectName = subjectName.ToLower().Trim();
            using (var context = new StudentBeleidContext())
            {
                _stagedSubjects.AddRange(
                    context.Subjects.Where(
                        s => (
                        s.Name.ToLower().Equals(subjectName) ||
                        s.SubjectCode.ToLower().Equals(subjectName)
                        ) && !_stagedSubjects.Contains(s)
                    ).ToList());
            }
        }

        // removes double found subjects
        public void RemoveDoubleSubjects()
        {
            _stagedSubjects.ToHashSet();
        }

        public void DisplaySubjectsResult()
        {
            using (var context = new StudentBeleidContext())
            {
                if (_stagedSubjects.Count > 0)
                {
                    SubjectResult.Content = _stagedSubjects.Count + " vak" + ((_stagedSubjects.Count > 1) ? "ken" : "") + " gevonden";
                }
                else
                {
                    SubjectResult.Content = "Geen vak(ken) gevonden";
                }
            }
        }

        public void UpdateButtonText()
        {
            if (_stagedSubjects.Count == 0 || _stagedStudents.Count == 0)
            {
                Verzenden.Content = "Voeg vak(ken) toe aan student(en)";
            }
            else Verzenden.Content = "Voeg vak" + (_stagedSubjects.Count > 1 ? "ken" : "") + " toe aan student" + (_stagedStudents.Count > 1 ? "en" : "");
        }
/*

        public void AddSubjectToStudents(Subject subject)
        {
            for (int i = 0; i < _stagedStudents.Count; i++)
            {
                if (!_stagedStudents[i].Subjects.Contains(subject)) _stagedStudents[i].Subjects.Append(subject);
            }
        }*/

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (_stagedStudents.Count == 0 || _stagedSubjects.Count == 0) return;

            using (var context = new StudentBeleidContext())
            {
                // loop through all selected students
                for (int i = 0; i < _stagedStudents.Count; i++)
                {
                    // find student in database which has the same id as the selected student
                    Student? dbStudent = context.Students.Where(s => s.Id == _stagedStudents[i].Id).First();
                    // debug student
                    Trace.WriteLine(dbStudent);
                    if (dbStudent != null)
                    {
                        // loop through all selected subjects
                        for (int j = 0; j < _stagedSubjects.Count; j++)
                        {
                            // find subject
                            Subject? dbSubject = context.Subjects.Where(s => s.Id == _stagedSubjects[j].Id).First();
                            // debug subject
                            Trace.WriteLine(dbSubject);
                            if (dbSubject != null)
                            {
                                // create new list of subjects if list is null
                                if (dbStudent.Subjects == null) dbStudent.Subjects = new List<Subject>();

                                /*dbStudent.Subjects = new List<Subject>();*/
                                Trace.WriteLine(dbStudent.Subjects.Count);

                                // add subject to list
                                if (!dbStudent.Subjects.Any(s => s.Id == dbSubject.Id) && !dbStudent.Subjects.Contains(dbSubject)) dbStudent.Subjects.Add(dbSubject);
                            }
                        }
                    }
                }
                // save changes to database
                context.SaveChanges();
            }

            using (var context = new StudentBeleidContext())
            {
                // loop through all selected students
                for (int i = 0; i < _stagedStudents.Count; i++)
                {
                    // find subject
                    Student? dbStudent = context.Students.Where(s => s.Id == _stagedStudents[i].Id).First();
                    if (dbStudent != null)
                    {
                        // create new list of subjects if list is null
                        if (dbStudent.Subjects == null) dbStudent.Subjects = new List<Subject>();
                        // loop through all selected subjects
                        Trace.WriteLine(dbStudent.Subjects.Count()); // ALWAYS 0
                        for (int j = 0; j < dbStudent.Subjects.Count(); j++)
                        {
                            // debug subject + student
                            string text = dbStudent.ToString() + dbStudent.Subjects.ElementAt(j).ToString();
                            Trace.WriteLine(text);
                        }
                    }

                }
            }

            CloseWindow();
        }

        // updating problem description
        private void OnClassChanged(object sender, TextChangedEventArgs e)
        {
            // find students
            FindStudents(ClassName.Text);

            // show result on screen
            DisplayStudentsResult();
        }

        private void OnSubjectChanged(object sender, TextChangedEventArgs e)
        {
            // find subjects
            FindSubjects(SubjectName.Text); 

            // show result on screen
            DisplaySubjectsResult();
        }

        // close problem submitting window
        private void CloseWindow()
        {
            Close();
        }
    }
}
