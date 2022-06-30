#region

using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Database;
using Database.Model;
using WPF.Screens;

#endregion

namespace WPF.Pages;

public partial class DetailscreenProblems : Page
{
    public Student SelectedStudent;
    
    public DetailscreenProblems(Student student)
    {
        InitializeComponent();
        SelectedStudent = student;
        
        using (var context = new DatabaseContext())
        {
            // create the problems list
            List<StudentProblem> problems = context.StudentProblem.Where(x => x.StudentId == SelectedStudent.Id).ToList();

            foreach (StudentProblem studentProblem in problems)
            {
                studentProblem.Teacher = context.Teacher.First(x => x.Id == studentProblem.TeacherId);
            }
            
            // add the problems to the table
            StudentProbleen.ItemsSource = problems;
            // set the header to match the student
            studentproblemsLbl.Content = $"problemen van student: {SelectedStudent.StudentNumber}";
        }
    }

    /**
     * <summary>Open new window to create problem</summary>
     */
    private void CreateNewProblemBtn(object sender, RoutedEventArgs e)
    {
        //Open new window to create problem
        ProblemSubmitting popup = new ProblemSubmitting(SelectedStudent.Id);
        popup.ShowDialog();
    }
}