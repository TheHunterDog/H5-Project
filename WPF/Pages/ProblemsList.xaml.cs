#region

using System.Linq;
using System.Windows.Controls;
using Database.Model;

#endregion

namespace WPF.Pages
{
    /**
     * <summary>Interaction logic for DetailscreenMeldingen.xaml</summary>
     */
    public partial class ProblemsList : Page
    {
        public ProblemsList()
        {
            InitializeComponent();
            using (var context = new DatabaseContext())
            {
                // join the tables so all the information van be shown in the grid
                var lijst = context.StudentProblem.Join(
                context.Student,
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
                // add the list to the table
                MeldingenTable.ItemsSource = lijst;
            }
        }
    }
}
