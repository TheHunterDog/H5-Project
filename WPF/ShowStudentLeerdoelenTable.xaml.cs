using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Windows;
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
    
    private void LeerdoelenToevoegen_OnClick(object sender, RoutedEventArgs e)
    {
        throw new System.NotImplementedException();
    }
    
    private void Search_OnClick(object sender, RoutedEventArgs e)
    {
        Leerdoelen = new List<Leerdoel>();
        using (StudentBeleidContext context = new StudentBeleidContext())// TODO: Change to App.Context
        {
            if (SearchInput.Text.Length == 0)
            {
                Leerdoelen = context.Leerdoelen.ToList();
            }
            else
            {
                List<int> match = SmartSearch.SmartSearchStudent(SearchInput.Text);
                foreach (var student in match)
                {
                    Leerdoelen = context.Leerdoelen.Where(s => s.Student.Id == student).ToList().Concat(Leerdoelen).ToList();

                } 
            }
        }
        StudentLeerdoelen.ItemsSource = Leerdoelen;
    }
}
