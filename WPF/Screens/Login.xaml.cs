#region

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using Database.Model;
using Microsoft.Toolkit.Uwp.Notifications;
using WPF.Util;

#endregion

namespace WPF.Screens
{
    /**
     * <summary>Interaction logic for Login.xaml</summary>
     */
    public partial class Login : Window
    {
        private int _failedBecauseEmpty = 0;
        private int _failedLogins = 0;
        private Timer _timer = new Timer();
        NotificationBroker b = new NotificationBroker(1);

        
        
        
        public Login()
        {
            InitializeComponent();
            _timer.Elapsed += delegate(object? sender, ElapsedEventArgs args) { OnTimedEvent(sender, args, Submit); };
            username.GotFocus += RemoveText;
            username.LostFocus += AddText;
            password.GotFocus += RemoveText;
            password.LostFocus += AddText;
        }
        
        /**
         * <summary>Remove the text from the textbox</summary>
         */
        public void RemoveText(object sender, EventArgs e)
        {
            TextBox btn = (TextBox) sender;
            if (btn.Text == btn.Name) 
            {
                btn.Text = "";
            }
        }
        /**
         * <summary>Add the text to a textbox</summary>
         */
        public void AddText(object sender, EventArgs e)
        {
            TextBox btn = (TextBox) sender;
            if (string.IsNullOrWhiteSpace(btn.Text))
                btn.Text = btn.Name;
        }
        
        /**
         * <summary>Button click logic</summary> 
         */
        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            // check if there is an input
            if ((username.Text.Length > 0 && password.Text.Length > 0 ) && (password.Text != password.Name && username.Text != username.Name))
            {
                // check if the user has the correct password
                IAuthenticatable? suspect = Authentication.GetUserWithCredentials(password.Text, username.Text, App.context);
                if (suspect != null &&Authentication.CheckPassword(Encoding.UTF8.GetBytes(password.Text), suspect.Password,Encoding.ASCII.GetBytes(Authentication.Salt)))
                {
                    // if all things match, open the main window
                    MainWindow m = new MainWindow(suspect);
                    m.Show();
                    Close();
                }
                else
                {
                    // password and username are incorrect
                    MessageBox.Show("Password or username are incorrect", "Login", MessageBoxButton.OK, MessageBoxImage.Error);
                    _failedLogins++;
                    if (_failedLogins > 5)
                    {
                        // disable the login button
                        _timer.Interval = 1000 * _failedLogins;
                        _timer.Enabled = true;
                        Submit.IsEnabled = false;
                    }
                }
            }
            else
            {
                // show message if the fields are empty
                _failedBecauseEmpty++;
                if (_failedBecauseEmpty > 3)
                {
                    MessageBox.Show("You need to fill both input fields", "Login", MessageBoxButton.OK, MessageBoxImage.Information);

                }
            }
        }

        /**
         * <summary>Function to stop submit spam</summary>
         */
        private void OnTimedEvent(object source, ElapsedEventArgs e,Button submit)
        {
            this._timer.Enabled = false;
            this.Dispatcher.Invoke(() =>
            {
                submit.IsEnabled = true;
            });
        }

        private void NotificationListen_OnClick(object sender, RoutedEventArgs e)
        {
            //
            DatabaseContext d = new DatabaseContext();
            // byte[] password = Authentication
            //     .HashPasswordWithSalt(Encoding.ASCII.GetBytes("a"), Encoding.ASCII.GetBytes(Authentication.Salt));
            // d.StudentSupervisor.Add(new StudentSupervisor()
            //     {IsAdmin = true, Name = "a", Password = Encoding.Default.GetString(password) ,Username = "a",TeacherCode = "a"});
            // d.SaveChanges();
            var studentSupervisor =d.StudentSupervisor.Where(s => s.Name == "egg").First();
            // foreach (var student in d.Student)
            // {
            //     // student.StudentSupervisor = studentSupervisor.Id;
            //     student.Supervisor = studentSupervisor;
            // }

            for (int i = 0; i < 30; i++)
            {
                d.Student.Add(new Student()
                {
                    FirstName = Faker.Name.First(),
                    ClassCode = "ICTOOSDD",
                    LastName = Faker.Name.Last(),
                    StudentNumber = $"s{Faker.RandomNumber.Next(0, 100000)}",
                    Supervisor = studentSupervisor,
                });
            }


            d.SaveChanges();

            // if (b.IsActive)
            // {
            //
            //     b.StopNotification();
            // }
            // else
            // {
            //     b.StartNotification();
            // }
        }

        private void NotificationTest_OnClick(object sender, RoutedEventArgs e)
        {
            DatabaseContext d = new DatabaseContext();
            d.Notifications.Add(new Notification() {Description = "TEST",Receiver = d.StudentSupervisor.First()});
            d.SaveChanges();
        }
    }
}
