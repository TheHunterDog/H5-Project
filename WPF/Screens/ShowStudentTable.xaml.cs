#region

using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Database.Model;

#endregion

namespace WPF.Screens;
/// <summary>
/// Interaction logic for ShowStudentTable.xaml
/// </summary>
public partial class ShowStudentTable
{
    public List<Student> Students;
    public ShowStudentTable()
    {
        InitializeComponent();

        using (var context = new DatabaseContext())
        {
            // put all the students in a list
            Students = context.Student.ToList();
        }
        // insert the students into the table
        StudentsTable.ItemsSource = Students;
    }

    /**
     * <summary>Select the row in the student table </summary>
     */
    private void selectRow(object sender, MouseButtonEventArgs e)
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