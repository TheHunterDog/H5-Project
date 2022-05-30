using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Database.Model;

namespace WPF;
public partial class ShowStudentTable

{
    public List<Student> Students;
    public ShowStudentTable()
    {
        InitializeComponent();

        using (var context = new StudentBeleidContext())
        {
            Students = context.Students.ToList();
        }
        StudentsTable.ItemsSource = Students;
    }

    private void selectRow(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        Student selectedStudent = (Student)StudentsTable.SelectedItem;
        if (selectedStudent != null)
        {
            Detailscreen detailScreen = new Detailscreen(selectedStudent);
            detailScreen.Show();
        }
    }
}