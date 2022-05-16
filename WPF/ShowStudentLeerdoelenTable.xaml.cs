using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Database.Model;

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
        using (StudentBeleidContext context = new StudentBeleidContext())// TODO: Change to App.Context
        {
            if (SearchInput.Text.Length == 0)
            {
                Leerdoelen = context.Leerdoelen.ToList();
            }
            else
            {
                Leerdoelen = context.Leerdoelen.Where(s => s.Student.Studentnummer == SearchInput.Text).ToList();
            }
        }

        StudentLeerdoelen.ItemsSource = Leerdoelen;
    }
}