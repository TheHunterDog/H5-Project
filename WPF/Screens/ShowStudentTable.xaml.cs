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
        Detailscreen detailscreen = new Detailscreen();
        Student selectedstudent = (Student)StudentsTable.SelectedItem;
        if (selectedstudent != null)
        {
            detailscreen.studentnr = selectedstudent.Studentnummer;
            detailscreen.addStudentInfo();
            detailscreen.Show();
        }
    }
}