using System.Collections.Generic;
using System.Linq;
using Database.Model;

namespace WPF;
/// <summary>
/// Interaction logic for ShowStudentTable.xaml
/// </summary>
public partial class ShowStudentTable
{
    public List<Student> Students;
    public ShowStudentTable()
    {
        InitializeComponent();

        using (var context = new StudentBeleidContext())
        {
            // put all the students in a list
            Students = context.Students.ToList();
        }
        // insert the students into the table
        StudentsTable.ItemsSource = Students;
    }

    /**
     * <summary>Select the row in the student table </summary>
     */
    private void selectRow(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        // get the selected student
        Student selectedStudent = (Student)StudentsTable.SelectedItem;
        if (selectedStudent != null)
        {
            // create new detailscreen
            Detailscreen detailScreen = new Detailscreen(selectedStudent);
            detailScreen.Show();
        }
    }
}