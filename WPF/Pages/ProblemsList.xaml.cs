using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Database.Model;

namespace WPF.Pages
{
    /// <summary>
    /// Interaction logic for DetailscreenMeldingen.xaml
    /// </summary>
    public partial class ProblemsList : Page
    {
        public ProblemsList()
        {
            InitializeComponent();
            using (var context = new StudentBeleidContext())
            {
                var lijst = context.StudentProblems.Join(
                context.Students,
                problem => problem.StudentId,
                student => student.Id,
                (problem, student) => new
                {
                   StudentNr = student.Studentnummer,
                   Naam = $"{student.Voornaam}{(" " + student.Tussenvoegsel).TrimEnd()} {student.Achternaam}",
                   Description = problem.Description,
                   Notifyer = problem.Teacher.Name,
                   Prioriteit = problem.Priority
                }).ToList().OrderBy(x=> x.StudentNr).OrderByDescending(x => x.Prioriteit);
                MeldingenTable.ItemsSource = lijst;
            }
        }
    }
}
