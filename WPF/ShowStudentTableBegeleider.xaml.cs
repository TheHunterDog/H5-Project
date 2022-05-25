using System.Linq;
using System.Windows;
using WPF.Model;

namespace WPF;

public partial class ShowStudentTableBegeleider : Window
{
    public ShowStudentTableBegeleider()
    {
        InitializeComponent();
        
        using (StudentBeleidContext context = new StudentBeleidContext())
        {
            var data = context.StudentBegeleiderGesprekken.Join(
                context.Students,
                StudentBegeleiderGesprekken => StudentBegeleiderGesprekken.StudentId,
                Students => Students.Id, ((gesprekken, student) => new
                {
                    Id = student.Id,
                    StudentNummer = student.Studentnummer,
                    Voornaam = student.Voornaam,
                    Achternaam = student.Achternaam,
                    Klascode = student.Klasscode,
                    LaatstGesproken = gesprekken.GesprekDatum,
                })).ToList();
            StudentListB.ItemsSource = data;
            
        }
        
        
    }
}