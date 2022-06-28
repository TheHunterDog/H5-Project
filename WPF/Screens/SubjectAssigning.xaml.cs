#region

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Database.Model;
using Microsoft.EntityFrameworkCore;

#endregion

namespace WPF.Screens
{
    /**
     * <summary>Interaction logic for SubjectAssigning.xaml</summary>
     */
    public partial class SubjectAssigning : Window
    {
        public List<Student> _stagedStudents;
        public List<Subject> _stagedSubjects;


        /**
        * <summary>Initialize problem on load window</summary>
        */
        public SubjectAssigning()
        {
            InitializeComponent();

            _stagedStudents = new List<Student>();
            _stagedSubjects = new List<Subject>();
        }

        /**
        * <summary>Finds students by query</summary>
        */
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

        /**
         * <summary>Finds all students in a class</summary>
         */
        public int FindClass(string className)
        {
            // set string to lowercase, and remove spaces
            className = className.ToLower().Trim();
            // retrieve count of found students before finding more students
            int count = _stagedStudents.Count;
            using (var context = new DatabaseContext())
            {
                // add students to list who are in the exact class, or a class that contains the string, if the string is more than 1 letter
                _stagedStudents.AddRange(
                    context.Student.Where(
                        s => (
                        s.ClassCode.ToLower().Equals(className) || 
                        (className.Length > 5 && s.ClassCode.ToLower().Contains(className))
                        ) && !_stagedStudents.Contains(s)
                    ).ToList());
            }
            // return new amount of students found minus old count
            return _stagedStudents.Count - count;
        }

        /**
         * <summary>Finds all students by name or number</summary>
         */
        public void FindStudent(string studentName)
        {
            // set string to lowercase
            studentName = studentName.ToLower();
            using (var context = new DatabaseContext())
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
                            context.Student.Where(
                                s => (
                                s.StudentNumber.ToLower().Equals(entries[0]) || 
                                s.FirstName.ToLower().Equals(entries[0]) ||
                                s.LastName.ToLower().Equals(entries[0])
                                ) && !_stagedStudents.Contains(s)
                            ).ToList());
                    }
                    // check if there are two entries
                    else if (entries.Count == 2)
                    {
                        // add students that have the entries as first name and last name
                        _stagedStudents.AddRange(
                            context.Student.Where(
                                s => (
                                s.FirstName.ToLower().Equals(entries[0]) ||
                                s.FirstName.ToLower().Equals(entries[1])
                                ) && (
                                s.LastName.ToLower().Equals(entries[0]) ||
                                s.LastName.ToLower().Equals(entries[1])
                                ) && !_stagedStudents.Contains(s)
                            ).ToList());
                    }
                    // check if there are three entries
                    else if (entries.Count == 3)
                    {
                        // add students that have the entries as first name, last name and surname
                        _stagedStudents.AddRange(
                            context.Student.Where(
                                s => (
                                s.FirstName.ToLower().Equals(entries[0]) ||
                                s.FirstName.ToLower().Equals(entries[1]) ||
                                s.FirstName.ToLower().Equals(entries[2])
                                ) && (
                                s.LastName.ToLower().Equals(entries[0]) ||
                                s.LastName.ToLower().Equals(entries[1]) ||
                                s.LastName.ToLower().Equals(entries[2])
                                ) && (
                                s.MiddleName.ToLower().Equals(entries[0]) ||
                                s.MiddleName.ToLower().Equals(entries[1]) ||
                                s.MiddleName.ToLower().Equals(entries[2])
                                ) && !_stagedStudents.Contains(s)
                            ).ToList());
                    }
                }
            }
        }

        /**
         * <summary>Removes double found students</summary>
         */
        public void RemoveDoubleStudents()
        {
            // finds unique students by override void ToHashSet in Student, which hashes the id of the student
            _stagedStudents.ToHashSet();
        }

        /**
         * <summary>Displays the found studetns</summary>
         */
        public void DisplayStudentsResult()
        {
            using (var context = new DatabaseContext())
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

        /**
         * <summary>Find the subject</summary>
         */
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

        /**
         * <summary>Find the subject</summary>
         */
        public void FindSubject(string subjectName)
        {
            subjectName = subjectName.ToLower().Trim();
            using (var context = new DatabaseContext())
            {
                _stagedSubjects.AddRange(
                    context.Subject.Where(
                        s => (
                        s.Name.ToLower().Equals(subjectName) ||
                        s.SubjectCode.ToLower().Equals(subjectName)
                        ) && !_stagedSubjects.Contains(s)
                    ).ToList());
            }
        }

        /**
         * <summary>Removes double found subjects</summary>
         */
        public void RemoveDoubleSubjects()
        {
            _stagedSubjects.ToHashSet();
        }

        /**
         * <summary>Displays the amount of found subjects</summary>
         */
        public void DisplaySubjectsResult()
        {
            using (var context = new DatabaseContext())
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

        /**
         * <summary>Update the text on the buttons</summary>
         */
        public void UpdateButtonText()
        {
            if (_stagedSubjects.Count == 0 || _stagedStudents.Count == 0)
            {
                Verzenden.Content = "Voeg vak(ken) toe aan student(en)";
            }
            else Verzenden.Content = "Voeg vak" + (_stagedSubjects.Count > 1 ? "ken" : "") + " toe aan student" + (_stagedStudents.Count > 1 ? "en" : "");
        }


        /**
         * <summary>Logic for submit click</summary>
         */
        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (_stagedStudents.Count == 0 || _stagedSubjects.Count == 0) return;

            using (var context = new DatabaseContext())
            {
                // loop through all selected students
                for (int i = 0; i < _stagedStudents.Count; i++)
                {
                    // find student in database which has the same id as the selected student
/*                    List<Student> allStudents = context.Student.Include(s => s.Subjects).ToList();
                    allStudents = allStudents.Where();*/
                    Student dbStudent = context.Student.Include(s => s.Subjects).Where(s => s.Id == _stagedStudents[i].Id).First();
                    // debug student
                    Trace.WriteLine(dbStudent);
                    Trace.WriteLine(dbStudent.Subjects.Count()); // 1
                    //dbStudent.Subjects = new[] {context.Subjects.First()};

                    for (int j = 0; j < _stagedSubjects.Count; j++)
                    {
                        Subject s = context.Subject.Where(s => s.Id == _stagedSubjects[j].Id).First();
                        Trace.WriteLine(s);
                        if (!dbStudent.Subjects.Contains(s))
                        {
                            dbStudent.Subjects.Add(s);
                        }
                        else
                        {
                            Trace.WriteLine("The Fuck!?!!?!?!?!"); // komt hier niet
                        }
                        Trace.WriteLine(dbStudent.Subjects.Count()); // nog steeds 1
                    }
                }
                // save changes to database
                context.SaveChanges();
            }

            using (var context = new DatabaseContext())
            {
                // loop through all selected students
                for (int i = 0; i < _stagedStudents.Count; i++)
                {
                    // find student
                    Student dbStudent = context.Student.Include(s => s.Subjects).Where(s => s.Id == _stagedStudents[i].Id).First();
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

        /**
         * <summary>Updating students search</summary>
         */
        private void OnClassChanged(object sender, TextChangedEventArgs e)
        {
            // find students
            FindStudents(ClassName.Text);

            // show result on screen
            DisplayStudentsResult();
        }

        /**
         * <summary>Updating subject search</summary>
         */
        private void OnSubjectChanged(object sender, TextChangedEventArgs e)
        {
            // find subjects
            FindSubjects(SubjectName.Text); 

            // show result on screen
            DisplaySubjectsResult();
        }

        /**
         * <summary>Closes window</summary>
         */
        private void CloseWindow()
        {
            Close();
        }
    }
}
