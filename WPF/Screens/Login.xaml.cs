using System;
using System.Linq;
using System.Text;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using Database.Model;
using WPF.Util;

namespace WPF.Screens
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private int _failedBecaouseEmpty = 0;
        private int _failedLogins = 0;
        private Timer _timer = new Timer();
        
        
        
        public Login()
        {
            InitializeComponent();
            _timer.Elapsed += delegate(object? sender, ElapsedEventArgs args) { OnTimedEvent(sender, args, Submit); };
            username.GotFocus += RemoveText;
            username.LostFocus += AddText;
            password.GotFocus += RemoveText;
            password.LostFocus += AddText;
        }
        
        public void RemoveText(object sender, EventArgs e)
        {
            TextBox btn = (TextBox) sender;
            if (btn.Text == btn.Name) 
            {
                btn.Text = "";
            }
        }

        public void AddText(object sender, EventArgs e)
        {
            TextBox btn = (TextBox) sender;
            if (string.IsNullOrWhiteSpace(btn.Text))
                btn.Text = btn.Name;
        }
        
        /**
         * <summary>button click logic</summary> 
         */
        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if ((username.Text.Length > 0 && password.Text.Length > 0 ) && (password.Text != password.Name && username.Text != username.Name))
            {
                IAuthenticatable? suspect = Authentication.GetUserWithCredentials(password.Text, username.Text, App.context);
                if (suspect != null &&Authentication.CheckPassword(Encoding.UTF8.GetBytes(password.Text), suspect.Password,Encoding.ASCII.GetBytes(Authentication.Salt)))
                {
                    MainWindow m = new MainWindow(suspect);
                    m.Show();
                    Close();
                }
                else
                {
                    MessageBox.Show("Password or username are incorrect", "Login", MessageBoxButton.OK, MessageBoxImage.Error);
                    _failedLogins++;
                    if (_failedLogins > 5)
                    {
                        _timer.Interval = 1000 * _failedLogins;
                        _timer.Enabled = true;
                        Submit.IsEnabled = false;
                    }
                }
            }
            else
            {
                _failedBecaouseEmpty++;
                if (_failedBecaouseEmpty > 3)
                {
                    MessageBox.Show("You need to fill both input fields", "Login", MessageBoxButton.OK, MessageBoxImage.Information);

                }
            }
        }

        private void FILLDATABASE_Click(object sender, RoutedEventArgs e)
        {
            String a = "admin";
            string salt = "APP";
            
            Teacher t = App.context.Teachers.First();
            t.Username = a;
            t.Password = System.Text.Encoding.Default.GetString(Authentication.HashPasswordWithSalt(Encoding.ASCII.GetBytes(a), Encoding.ASCII.GetBytes(salt)));
            App.context.SaveChanges();

           IAuthenticatable b = Authentication.GetUserWithCredentials("admin", "admin", App.context);
           bool check = Authentication.CheckPassword(Encoding.UTF8.GetBytes(a), b.Password,
                Encoding.ASCII.GetBytes(salt));
        }


        private void OnTimedEvent(object source, ElapsedEventArgs e,Button submit)
        {
            this._timer.Enabled = false;
            this.Dispatcher.Invoke(() =>
            {
                submit.IsEnabled = true;
            });
        }
    }
}
