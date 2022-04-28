using System.Collections.Generic;
using System.Linq;
using System.Windows;
using WPF.Model;

namespace WPF;

public partial class ShowStudentTable : Window
{
    public List<Student> Students { get; set; }
    public ShowStudentTable()
    {
        InitializeComponent();

        using (StudentBeleidContext context = new StudentBeleidContext())
        {
            Students = context.Students.ToList();
        }
    }

     
}