using System.Linq;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using WPF.Model;

namespace WPF;

public partial class ShowStudentTableBegeleider : Window
{
    public ShowStudentTableBegeleider()
    {
        InitializeComponent();
        
        using (StudentBeleidContext context = new StudentBeleidContext())
        {
            var data = context.Students.Include(g => g.StudentBegeleiderGesprekken).ToList();

            StudentListB.ItemsSource = data;
        }
        
        
    }
}