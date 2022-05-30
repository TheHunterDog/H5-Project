using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Threading;
using Database.Model;
using Microsoft.Win32;
//using DataImporting;

namespace WPF.Screens;

public partial class StudentTable : Page
{
    public List<Student> Students;
    public StudentTable()
    {
        InitializeComponent();

        using (var context = new StudentBeleidContext())
        {
            Students = context.Students.ToList();
        }
        StudentsTable.ItemsSource = Students;
    }

    private void SelectRow(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        Student selectedStudent = (Student) StudentsTable.SelectedItem;
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

        //ExcelImporter.ImportStudentsFromFile(directory);
        //ExcelImporter.PrintStudents();
    }
}