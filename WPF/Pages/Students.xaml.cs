#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Database;
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
            var students = context.Student.ToList();
            foreach (Student student in students)
            {
                student.SupervisorMeetings = context.StudentSupervisorMeeting
                    .Where(x => x.Student.Id == student.Id).ToList();
            }

            // set the source of the dataGrid to the list
            StudentDetail.ItemsSource = students;
        }
    }

    /**
     * <summary>Select a row and open the detailScreen</summary>
     */
    private void SelectRow(object sender, MouseButtonEventArgs e)
    {
        var selectedItem = StudentDetail.SelectedItem;
        // get the studentID from the selected item
        int selectedStudentId = int.Parse(selectedItem.GetType().GetProperty("Id").GetValue(selectedItem).ToString());
        Student selectedStudent;
        using (var context = new DatabaseContext())
        {
            // get the student from the selected ID
            selectedStudent = context.Student.First(x => x.Id == selectedStudentId);
        }

        // open a new detailScreen
        Detailscreen detailScreen = new Detailscreen(selectedStudent);
        detailScreen.Show();
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