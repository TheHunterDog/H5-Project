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
using System.Windows.Shapes;
using Database.Model;

namespace WPF.Screens
{
    /// <summary>
    /// Interaction logic for SBerToevoegen.xaml
    /// </summary>
    public partial class SBerToevoegen : Window
    {
        public SBerToevoegen()
        {
            InitializeComponent();
        }

        private void SubmitBtn(object sender, RoutedEventArgs e)
        {
            if (name.Text == "") return;
            if (Docentcode.Text == "") return;
            using (var context = new StudentBeleidContext())
            {
                if (IsSber.IsChecked == true)
                {
                    StudentBegeleider begeleider = new StudentBegeleider
                    {
                        Naam = name.Text,
                        Docentcode = Docentcode.Text
                    };
                    context.StudentBegeleiders.Add(begeleider);
                }
                else
                {
                    Teacher teacher = new Teacher
                    {
                        Name = name.Text
                        //Docentcode = Docentcode.Text          Docentcode bestaat nog niet bij teacher
                    };
                    context.Teachers.Add(teacher);
                }
                context.SaveChanges();
            }
            Close();
        }
    }
}
