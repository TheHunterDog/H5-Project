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

public partial class ShowStudentLeerdoelenTable : Page
{
    public List<LearningGoal> Leerdoelen { get; set; }
    public Student SelectedStudent;
    public ShowStudentLeerdoelenTable(Student st)
    {
        InitializeComponent();
        SelectedStudent = st;
        using (var context = new DatabaseContext())
        {
            // get all leerdoelen and put them in the table
            List<LearningGoal> leerdoelen = context.LearningGoals.Where(x => x.StudentId == SelectedStudent.Id).ToList();
            StudentLeerdoelen.ItemsSource = leerdoelen;
            studentnrlbl.Content = $"LearningGoals van Student: {SelectedStudent.StudentNumber}";
        }
    }

    /**
     * <summary>Opens the window to add leerdoelen</summary>
     */
    private void AddLeerdoelbtn(object sender, RoutedEventArgs e)
    {
        // open window to add leerdoelen
        StudentLeerdoelenToevoegen toevoegen = new StudentLeerdoelenToevoegen(SelectedStudent);
        toevoegen.Show();
    }
}
