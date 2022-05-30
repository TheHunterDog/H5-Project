using Database.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WPF.Screens;

namespace WPF.Pages
{
    /// <summary>
    /// Interaction logic for ManageCoaches.xaml
    /// </summary>
    public partial class ManageCoaches : Page
    {
        public List<StudentBegeleider> coaches;
        public ManageCoaches()
        {
            InitializeComponent();

            using (var context = new StudentBeleidContext())
            {
                coaches = context.StudentBegeleiders.ToList();
            }
            CoachesList.ItemsSource = coaches;
        }

        private void AddCoachBtn(object sender, RoutedEventArgs e)
        {
            SBerToevoegen test = new SBerToevoegen();
            test.Show();
        }
    }
}
