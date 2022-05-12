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

        using (StudentBeleidContext context = new StudentBeleidContext())// TODO: Change to App.Context
        {
            Leerdoelen = context.Leerdoelen.ToList();
        }

        StudentLeerdoelen.ItemsSource = Leerdoelen;


    }

     
}