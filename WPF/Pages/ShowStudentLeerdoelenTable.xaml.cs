using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using Database.Migrations;
using Database.Model;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WPF.Util;

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

            /*List<Leerdoel> leerdoelen = context.Leerdoelen.Where(x => x.StudentId == SelectedStudent.Id).ToList();
            StudentLeerdoelen.ItemsSource = leerdoelen;
            studentnrlbl.Content = $"Leerdoelen van Student: {SelectedStudent.Studentnummer}";*/
        }
    }

    private void AddLeerdoelbtn(object sender, RoutedEventArgs e)
    {
        StudentLeerdoelenToevoegen toevoegen = new StudentLeerdoelenToevoegen(SelectedStudent);
        toevoegen.Show();
    }
}
