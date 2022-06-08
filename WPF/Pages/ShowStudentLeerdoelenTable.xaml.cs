using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Database.Model;

namespace WPF;

public partial class ShowStudentLeerdoelenTable : Page
{
    public List<Leerdoel> Leerdoelen { get; set; }
    public Student SelectedStudent;
    public ShowStudentLeerdoelenTable(Student st)
    {
        InitializeComponent();
        SelectedStudent = st;
        using (var context = new StudentBeleidContext())
        {
            // get all leerdoelen and put them in the table
            List<Leerdoel> leerdoelen = context.Leerdoelen.Where(x => x.StudentId == SelectedStudent.Id).ToList();
            StudentLeerdoelen.ItemsSource = leerdoelen;
            studentnrlbl.Content = $"Leerdoelen van Student: {SelectedStudent.Studentnummer}";
        }
    }

    /**
     * <summary>Opens the window to add leerdoelen</summary>
     */
    private void AddLeerdoelbtn(object sender, RoutedEventArgs e)
    {
        // open window to add leerdoelen
        StudentLeerdoelenToevoegen toevoegen = new StudentLeerdoelenToevoegen(SelectedStudent);
        toevoegen.Show();
    }
}
