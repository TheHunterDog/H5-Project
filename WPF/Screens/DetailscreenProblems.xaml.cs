﻿
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Database.Model;

namespace WPF;

public partial class DetailscreenProblems : Window
{
    public Student SelectedStudent;
    
    public DetailscreenProblems(Student student)
    {
        InitializeComponent();
        SelectedStudent = student;
        
        using (var context = new StudentBeleidContext())
        {

            List<StudentProblem> problems = context.StudentProblems.Where(x => x.StudentId == SelectedStudent.Id).ToList();
            foreach (StudentProblem studentProblem in problems)
            {
                studentProblem.Teacher = context.Teachers.Find(studentProblem.TeacherId);
                // studentProblem.Teacher = new Teacher({Id = 1, Name = "Johan"});
            }

            StudentProbleen.ItemsSource = problems;
        }
    }
}