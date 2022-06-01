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
    /// Interaction logic for ProblemSubmitting.xaml
    /// </summary>
    public partial class SubjectAssigning : Window
    {
        private List<Student> _studentsInClass;

        // initialize problem on load window
        public SubjectAssigning()
        {
            InitializeComponent();
        }

        public int FindClass(string className)
        {
            className = className.ToLower().Trim();
            int count = _studentsInClass.Count;
            using (var context = new StudentBeleidContext())
            {
                /*students = context.Students.ToList();
                for (int i = 0; i < students.Count; i++)
                {
                    if (students[i].Klasscode.ToLower().Equals(className)) AddStudentToStagedList(students[i]);
                    else if (className.Length > 1 && students[i].Klasscode.ToLower().Contains(className)) AddStudentToStagedList(students[i]);
                }*/

                _studentsInClass.AddRange(
                    context.Students.Where(
                        s => (
                        s.Klasscode.ToLower().Equals(className) || 
                        (className.Length > 1 && s.Klasscode.ToLower().Contains(className))
                        ) && !_studentsInClass.Contains(s)
                    ).ToList());
            }
            return _studentsInClass.Count - count;
        }

        public void FindStudent(string studentName)
        {
            studentName = studentName.ToLower();
            //List<Student> students = new List<Student>();
            //_studentsInClass = new List<Student>();
            using (var context = new StudentBeleidContext())
            {
/*                students = context.Students.ToList();
                for (int i = 0; i < students.Count; i++)
                {*/
                    List<String> name = studentName.Split(" ").ToList();
                    for (int j = 0; j < name.Count; j++)
                    {
                        name[j].Trim();
                        if (name[j].Equals(""))
                        {
                            name.RemoveAt(j);
                            j--;
                        }
                    }

                    if(name.Count > 0)
                    {
                        if (name.Count == 1)
                        {
                            int number;
                            bool isNumeric = int.TryParse(name[0], out number);
                            if (isNumeric)
                            {
                                name[0] = "s" + name[0];
/*
                                if (students[i].Studentnummer.ToLower().Equals(name[0])) AddStudentToStagedList(students[i]);*/

                                _studentsInClass.AddRange(
                                    context.Students.Where(
                                        s => s.Studentnummer.ToLower().Equals(name[0])
                                    ).ToList());
                            }
                            else
                            {
                                _studentsInClass.AddRange(
                                    context.Students.Where(
                                        s => (
                                        s.Voornaam.ToLower().Equals(name[0]) ||
                                        s.Achternaam.ToLower().Equals(name[0])
                                        ) && !_studentsInClass.Contains(s)
                                    ).ToList());
                            }
/*                            else if (students[i].Voornaam.ToLower().Equals(name[0])) AddStudentToStagedList(students[i]);
                            else if (students[i].Achternaam.ToLower().Equals(name[0])) AddStudentToStagedList(students[i]);*/
                        }
                        else if (name.Count == 2)
                        {
                            _studentsInClass.AddRange(
                                context.Students.Where(
                                    s => (
                                    s.Voornaam.ToLower().Equals(name[0]) ||
                                    s.Voornaam.ToLower().Equals(name[1])
                                    ) && (
                                    s.Achternaam.ToLower().Equals(name[0]) ||
                                    s.Achternaam.ToLower().Equals(name[1])
                                    ) && !_studentsInClass.Contains(s)
                                ).ToList());

/*                            if (students[i].Voornaam.ToLower().Equals(name[0]) || students[i].Voornaam.ToLower().Equals(name[1]))
                            {
                                if (students[i].Achternaam.ToLower().Equals(name[0]) || students[i].Achternaam.ToLower().Equals(name[1]))
                                {
                                    AddStudentToStagedList(students[i]);
                                }
                            }*/
                        }
                        else if (name.Count == 3)
                        {
                            _studentsInClass.AddRange(
                                context.Students.Where(
                                    s => (
                                    s.Voornaam.ToLower().Equals(name[0]) ||
                                    s.Voornaam.ToLower().Equals(name[1]) ||
                                    s.Voornaam.ToLower().Equals(name[2])
                                    ) && (
                                    s.Achternaam.ToLower().Equals(name[0]) ||
                                    s.Achternaam.ToLower().Equals(name[1]) ||
                                    s.Achternaam.ToLower().Equals(name[2])
                                    ) && (
                                    s.Tussenvoegsel.ToLower().Equals(name[0]) ||
                                    s.Tussenvoegsel.ToLower().Equals(name[1]) ||
                                    s.Tussenvoegsel.ToLower().Equals(name[2])
                                    ) && !_studentsInClass.Contains(s)
                                ).ToList());

/*                            if (students[i].Voornaam.ToLower().Equals(name[0]) || students[i].Voornaam.ToLower().Equals(name[1]) || students[i].Voornaam.ToLower().Equals(name[2]))
                            {
                                if (students[i].Achternaam.ToLower().Equals(name[0]) || students[i].Achternaam.ToLower().Equals(name[1]) || students[i].Achternaam.ToLower().Equals(name[2]))
                                {
                                    if (students[i].Tussenvoegsel.ToLower().Equals(name[0]) || students[i].Tussenvoegsel.ToLower().Equals(name[1]) || students[i].Tussenvoegsel.ToLower().Equals(name[2]))
                                    {
                                        AddStudentToStagedList(students[i]);
                                    }
                                }
                            }*/
                        }
                    }
                //}
            }
        }

        public void AddStudentToStagedList(Student student)
        {
            if(!_studentsInClass.Contains(student)) _studentsInClass.Add(student);
        }

        public void RemoveDoubleStudents()
        {
            //_studentsInClass = _studentsInClass.Distinct().ToList();

            _studentsInClass.ToHashSet();
        }

        public void AddSubjectToStudents(Subject subject)
        {
            for (int i = 0; i < _studentsInClass.Count; i++)
            {
                if (!_studentsInClass[i].Subjects.Contains(subject)) _studentsInClass[i].Subjects.Append(subject);
            }
        }

        // submitting problem
        private void Submit_Click(object sender, RoutedEventArgs e)
        {
/*            if (_stagedProblem == null || _stagedProblem.Description == "") return;

            // Trace.WriteLine(_stagedProblem);

            AddProblemToDatabase(_stagedProblem);

            CloseWindow();*/
        }

        // updating problem description
        private void OnClassChanged(object sender, TextChangedEventArgs e)
        {
            // find students

            bool isClass = true;

            _studentsInClass = new List<Student>();

            List<String> entries = ClassName.Text.Split(",").ToList();
            for (int j = 0; j < entries.Count; j++)
            {
                entries[j].Trim();

                if (entries[j].Equals(""))
                {
                    entries.RemoveAt(j);
                    j--;
                }
/*                else
                {
                    Trace.WriteLine(classEntries[j]);
                }*/
            }

            for (int j = 0; j < entries.Count; j++)
            {
                int count = FindClass(entries[j]);
                if (count == 0)
                {
                    FindStudent(entries[j]);
                }
            }

            //FindClass(ClassName.Text);
/*
            if (_studentsInClass.Count == 0)
            {
                isClass = false;

                List<String> studentEntries = ClassName.Text.Split(",").ToList();
                for (int j = 0; j < studentEntries.Count; j++)
                {
                    studentEntries[j].Trim();

                    if (studentEntries[j].Equals(""))
                    {
                        studentEntries.RemoveAt(j);
                        j--;
                    }
*//*                    else
                    {
                        Trace.WriteLine(studentEntries[j]);
                    }*//*
                }

                for (int j = 0; j < studentEntries.Count; j++)
                {
                    FindStudent(studentEntries[j]);
                }
            }*/

            RemoveDoubleStudents();

            // show result on screen
            using (var context = new StudentBeleidContext())
            {
                if (_studentsInClass.Count > 0 && isClass)
                {
                    ClassResult.Content = "Klas gevonden(" + _studentsInClass.Count + ")";
                }
                else if (_studentsInClass.Count > 0 && !isClass)
                {
                    ClassResult.Content = "Student" + ((_studentsInClass.Count > 1) ? "en" : "") + " gevonden(" + _studentsInClass.Count + ")";
                }
                else
                {
                    ClassResult.Content = "Geen klas of student gevonden";
                }

            }
        }


        // close problem submitting window
        private void CloseWindow()
        {
            Close();
        }
    }
/*
    public class Problem
    {
        public int StudentID;
        public string Description = "Description";
        public int Priority = 0;
        public int TeacherID;

        public override string ToString()
        {
            return $"{StudentID}, {Description}, {Priority}, {TeacherID}";
        }
    }*/

}
