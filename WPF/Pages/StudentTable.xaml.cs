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

        using (var context = new StudentBeleidContext())
        {
            // a query to show to last seen date in the datagrid
            var query = from s in context.Students
                        join g in context.StudentBegeleiderGesprekken
                        on s.Id equals g.StudentId
                        into studentgesprekGroup
                        from t in studentgesprekGroup.DefaultIfEmpty()
                        select new
                        {
                            Id = s.Id,
                            Studentnummer = s.Studentnummer,
                            Voornaam = s.Voornaam,
                            Tussenvoegsel = s.Tussenvoegsel,
                            Achternaam = s.Achternaam,
                            Klasscode = s.Klasscode,
                            StudentbegeleiderId = s.StudentbegeleiderId,
                            LaatstGesproken = (t.GesprekDatum == null ? DateTime.MaxValue : t.GesprekDatum)
                        };
            var List = query.ToList().DistinctBy(x => x.Id).OrderBy(x => x.LaatstGesproken);
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
        using (var context = new StudentBeleidContext())
        {
            // get the student from the selected ID
            selectedStudent = context.Students.Where(x => x.Id == SelectedStudentID).First();
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