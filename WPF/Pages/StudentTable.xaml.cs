#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Database.Model;
using DataImporting;
using Microsoft.Win32;
using WPF.Screens;

#endregion

//using DataImporting;

namespace WPF.Pages;

public partial class StudentTable : Page
{
    public List<Student> Students;

    public StudentTable()
    {
        InitializeComponent();

        using (var context = new DatabaseContext())
        {
            // a query to show to last seen date in the datagrid
            var query = from s in context.Student
                        join g in context.StudentSupervisorMeeting
                        on s.Id equals g.StudentId
                        into studentgesprekGroup
                        from t in studentgesprekGroup.DefaultIfEmpty()
                        select new
                        {
                            Id = s.Id,
                            StudentNumber = s.StudentNumber,
                            FirstName = s.FirstName,
                            MiddleName = s.MiddleName,
                            LastName = s.LastName,
                            ClassCode = s.ClassCode,
                            StudentSupervisor = s.StudentSupervisor,
                            //LastMeeting = t.MeetingDate == null ? DateTime.MaxValue : t.MeetingDate
                        };
            var List = query.ToList().DistinctBy(x => x.Id);//.OrderBy(x => x.LastMeeting); ;
            // set the source of the datagrid to the list
            StudentsTable.ItemsSource = List;
        }

    }

    /**
     * <summary>Select a row and open the detailscreen</summary>
     */
    private void SelectRow(object sender, MouseButtonEventArgs e)
    {
        
        var SelectedItem = StudentsTable.SelectedItem;
        // get the studentID from the selected item 
        var SelectedStudentID = int.Parse(SelectedItem.GetType().GetProperty("Id").GetValue(SelectedItem).ToString());
        Student selectedStudent;
        using (var context = new DatabaseContext())
        {
            // get the student from the selected ID
            selectedStudent = context.Student.Where(x => x.Id == SelectedStudentID).First();
        }
        // check if there is a student selected
        if (selectedStudent != null)
        {   
            // open a new detailscreen
            Detailscreen detailScreen = new Detailscreen(selectedStudent);
            detailScreen.Show();
        }
    }

    /**
     * <summary>Import an excel file and store in students</summary>
     */
    private void ImportExcelBtn(object sender, RoutedEventArgs e)
    {
        // select the excel file to import
        OpenFileDialog openFileDialog = new OpenFileDialog();
        openFileDialog.Filter = "Excel Files(.xlsx)|*.xlsx";
        openFileDialog.ShowDialog();

        string directory = openFileDialog.FileName;
        // import into students
        ExcelImporter.ImportStudentsFromFile(directory);
    }
}