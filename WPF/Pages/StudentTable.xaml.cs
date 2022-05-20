using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using Database.Model;

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
}