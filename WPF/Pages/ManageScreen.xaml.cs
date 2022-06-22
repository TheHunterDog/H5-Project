#region

using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Database.Model;
using WPF.Screens;

#endregion

namespace WPF.Pages
{
    /**
     * <summary>Interaction logic for ManageCoaches.xaml</summary>
     */
    public partial class ManageCoaches : Page
    {
        public List<StudentSupervisor> coaches;
        public List<Teacher> teachers;
        public ManageCoaches()
        {
            InitializeComponent();

            using (var context = new StudentBeleidContext())
            {
                // make a coaches and teachers list
                coaches = context.StudentSupervisors.ToList();
                teachers = context.Teachers.ToList();
            }
            // set the table to display coaches at default
            CoachesList.ItemsSource = coaches;
            PromoteCol.Visibility = Visibility.Collapsed;
            
        }

        /**
        * <summary>Button to add new coach</summary>
        */
        private void AddCoachBtn(object sender, RoutedEventArgs e)
        {
            // open screen to add coach
            SBerToevoegen test = new SBerToevoegen();
            test.Show();
        }

        /**
        * <summary>Table to show teachers</summary>
        */
        private void ShowTeachers(object sender, RoutedEventArgs e)
        {
            // change to title of the page
            TitelLbl.Content = "Beheer Docenten:";
            // set the table to display teachers
            CoachesList.ItemsSource = teachers;
            // show the promote button and hide the demote button
            PromoteCol.Visibility = Visibility.Visible;
            DemoteCol.Visibility = Visibility.Collapsed;
            // change the binding to show to teacher name and docentcode
            name.Binding = new Binding("Name");
            Docentcode.Binding = new Binding("DocentCode");
        }

        /**
        * <summary>Table to show coaches</summary>
        */
        private void ShowCoaches(object sender, RoutedEventArgs e)
        {
            // change to title of the page
            TitelLbl.Content = "Beheer Student Begeleiders:";
            // set the table to display coaches
            CoachesList.ItemsSource = coaches;
            // show the demote button and hide the promote button
            DemoteCol.Visibility = Visibility.Visible;
            PromoteCol.Visibility = Visibility.Collapsed;
            // change the binding to show to coach name and docentcode
            name.Binding = new Binding("Name");
            Docentcode.Binding = new Binding("TeacherCode");

        }

        /**
        * <summary>Logic to promote a teacher to sber</summary>
        */
        private void PromoteTeacher(object sender, RoutedEventArgs e)
        {
            // select teacher to promote
            Teacher selectedTeacher = (Teacher)CoachesList.SelectedItem;
            // open new screen to promote
            promoteDemoteTeacher promoteDemoteTeacher = new promoteDemoteTeacher(true, teacher: selectedTeacher);
            promoteDemoteTeacher.Show();
        }

        /**
        * <summary>Logic to demote coach to a teacher</summary>
        */
        private void DemoteTeacher(object sender, RoutedEventArgs e)
        {
            // select coach to demote
            StudentSupervisor selectedSber = (StudentSupervisor)CoachesList.SelectedItem;
            // open demote screen
            promoteDemoteTeacher promoteDemoteTeacher = new promoteDemoteTeacher(false, sber: selectedSber);
            promoteDemoteTeacher.Show();
        }

        /**
        * <summary>Open new add subject window</summary>
        */
        private void ButtonAddSubject_Click(object sender, RoutedEventArgs e)
        {
            //open new add subject window
            AddSubject win = new AddSubject();
            win.Show();
        }

        /**
        * <summary>Open window to couple subject to student</summary>
        */
        private void ButtonCoupleSubject_Click(object sender, RoutedEventArgs e)
        {
            //open window to couple subject to student
            SubjectAssigning win = new SubjectAssigning();
            win.Show();
        }
    }
}
