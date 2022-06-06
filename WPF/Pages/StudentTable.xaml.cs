using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Threading;
using Database.Model;
using Microsoft.Win32;

namespace WPF.Screens;

public partial class StudentTable : Page
{
    public List<Student> Students;
    public StudentTable()
    {
        InitializeComponent();

        using (var context = new StudentBeleidContext())
        {
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
            StudentsTable.ItemsSource = List;
        }
        
    }

    private void SelectRow(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        var SelectedItem = StudentsTable.SelectedItem;
        var SelectedStudentID = int.Parse(SelectedItem.GetType().GetProperty("Id").GetValue(SelectedItem).ToString());
        Student selectedStudent;
        using (var context = new StudentBeleidContext())
        {
            selectedStudent = context.Students.Where(x => x.Id == SelectedStudentID).First();
        }
        if (selectedStudent != null)
        {
            Detailscreen detailScreen = new Detailscreen(selectedStudent);
            detailScreen.Show();
        }
    }

    private void ImportExcelBtn(object sender, System.Windows.RoutedEventArgs e)
    {
        OpenFileDialog openFileDialog = new OpenFileDialog();
        openFileDialog.Filter = "Excel Files(.xlsx)|*.xlsx";
        openFileDialog.ShowDialog();

        string directory = openFileDialog.FileName;
        /*Trace.WriteLine(directory);*/

        ExcelImporter.ImportStudentsFromFile(directory);
        ExcelImporter.PrintStudents();
    }
}