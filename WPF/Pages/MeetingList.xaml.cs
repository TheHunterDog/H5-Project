#region

using System.Linq;
using System.Windows.Controls;
using Database.Model;

#endregion

namespace WPF.Pages;

public partial class MeetingList : Page
{
    public MeetingList()
    {
        InitializeComponent();
        using (StudentBeleidContext context = new StudentBeleidContext())
        {
            // join the tables so all the information van be shown in the grid
            var lijst = context.StudentSupervisorMeetings.Where(x => x.StudentSupervisorId == 81).Join(
                context.Students,
                begeleider => begeleider.StudentId, 
                student => student.Id, 
                (begeleider, student) => new
                {
                    GesprekDatum = begeleider.MeetingDate,
                    StudentId = begeleider.StudentId,
                    Student = student,
                    Opmerkingen= begeleider.Comments,
                    Voornaam = student.Voornaam
                }).ToList();
            // add the list to the table
            MeetingTable.ItemsSource = lijst;
        }
        
    }
}