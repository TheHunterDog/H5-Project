using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Windows;
using Database.Migrations;
using Database.Model;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WPF.Util;

namespace WPF;

public partial class ShowStudentLeerdoelenTable : Window
{
    public List<Leerdoel> Leerdoelen { get; set; }
    public ShowStudentLeerdoelenTable()
    {
        InitializeComponent();
    }
/// <summary>
/// Search student based on searchfield <see cref="SmartSearch.SmartSearchStudent"/>
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>
    private void Search_OnClick(object sender, RoutedEventArgs e)
    {
        Leerdoelen = new List<Leerdoel>();

            if (SearchInput.Text.Length == 0)
            {
                Leerdoelen = App.context.Leerdoelen.ToList();
            }
            else
            {
                List<Student> match = SmartSearch.SmartSearchStudent(SearchInput.Text,App.context);
                foreach (var student in match)
                {
                    Leerdoelen = Leerdoelen.Concat(student.Leerdoelen) as List<Leerdoel>;
                }
            }
        StudentLeerdoelen.ItemsSource = Leerdoelen;
    }
}
