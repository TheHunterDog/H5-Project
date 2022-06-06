using Database.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using WPF.Screens;

namespace WPF.Pages
{
    /// <summary>
    /// Interaction logic for ManageCoaches.xaml
    /// </summary>
    public partial class ManageCoaches : Page
    {
        public List<StudentBegeleider> coaches;
        public List<Teacher> teachers;
        public ManageCoaches()
        {
            InitializeComponent();

            using (var context = new StudentBeleidContext())
            {
                coaches = context.StudentBegeleiders.ToList();
                teachers = context.Teachers.ToList();
            }
            CoachesList.ItemsSource = coaches;
            PromoteCol.Visibility = Visibility.Collapsed;
            
        }

        private void AddCoachBtn(object sender, RoutedEventArgs e)
        {
            SBerToevoegen test = new SBerToevoegen();
            test.Show();
        }

        private void ShowTeachers(object sender, RoutedEventArgs e)
        {
            TitelLbl.Content = "Beheer Docenten:";
            CoachesList.ItemsSource = teachers;
            PromoteCol.Visibility = Visibility.Visible;
            DemoteCol.Visibility = Visibility.Collapsed;
            name.Binding = new Binding("Name");
            Docentcode.Binding = new Binding("DocentCode");
        }

        private void ShowCoaches(object sender, RoutedEventArgs e)
        {
            TitelLbl.Content = "Beheer Student Begeleiders:";
            CoachesList.ItemsSource = coaches;
            DemoteCol.Visibility = Visibility.Visible;
            PromoteCol.Visibility = Visibility.Collapsed;
            name.Binding = new Binding("Naam");
            Docentcode.Binding = new Binding("Docentcode");

        }

        private void PromoteTeacher(object sender, RoutedEventArgs e)
        {
            Teacher selectedTeacher = (Teacher)CoachesList.SelectedItem;
            promoteDemoteTeacher promoteDemoteTeacher = new promoteDemoteTeacher(true, teacher: selectedTeacher);
            promoteDemoteTeacher.Show();
        }

        private void DemoteTeacher(object sender, RoutedEventArgs e)
        {
            StudentBegeleider selectedSber = (StudentBegeleider)CoachesList.SelectedItem;
            promoteDemoteTeacher promoteDemoteTeacher = new promoteDemoteTeacher(false, sber: selectedSber);
            promoteDemoteTeacher.Show();
        }
        private void ButtonAddSubject_Click(object sender, RoutedEventArgs e)
        {
            AddSubject win = new AddSubject();
            win.Show();
        }

        private void ButtonCoupleSubject_Click(object sender, RoutedEventArgs e)
        {
            SubjectAssigning win = new SubjectAssigning();
            win.Show();
        }
    }
}
