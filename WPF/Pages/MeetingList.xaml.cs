using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using Database.Model;

namespace WPF.Pages;

public partial class AnotherPage : Page
{
    public List<StudentBegeleiderGesprekken> Meetings;
    StudentBegeleider test;
    public AnotherPage()
    {
        InitializeComponent();
        using (StudentBeleidContext context = new StudentBeleidContext())
        {
            test = context.StudentBegeleiders.First();
            Meetings = context.StudentBegeleiderGesprekken.Where(x => x.StudentBegeleiderId == 81).ToList();
        }
        MeetingTable.ItemsSource = Meetings;
    }
}