using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using Database.Model;

namespace WPF.Pages;

public partial class MeetingList : Page
{
    public MeetingList()
    {
        InitializeComponent();
        using (StudentBeleidContext context = new StudentBeleidContext())
        {
            var lijst = context.StudentBegeleiderGesprekken.Where(x => x.StudentBegeleiderId == 81).Join(
                context.Students,
                begeleider => begeleider.StudentId, 
                student => student.Id, 
                (begeleider, student) => new
                {
                    GesprekDatum = begeleider.GesprekDatum,
                    StudentId = begeleider.StudentId,
                    Student = student,
                    Opmerkingen= begeleider.Opmerkingen,
                    Voornaam = student.Voornaam
                }).ToList();
            MeetingTable.ItemsSource = lijst;
        }
        
    }
}