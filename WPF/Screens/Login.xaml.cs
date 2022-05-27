using System;
using System.Linq;
using System.Text;
using System.Windows;
using Database.Model;
using WPF.Util;

namespace WPF.Screens
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }
        
        /**
         * <summary>button click logic</summary> 
         */
        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (username.Text.Length > 0 && password.Text.Length > 0)
            {
                IAuthenticatable suspect = Authentication.GetUserWithCredentials(password.Text, username.Text, App.context);
                if (Authentication.CheckPassword(Encoding.UTF8.GetBytes(password.Text), suspect.Password,Encoding.ASCII.GetBytes(Authentication.Salt)))
                {
                    App.User = suspect;
                    MainWindow m = new MainWindow(suspect);
                    m.Show();
                    Close();
                }
                else
                {
                    //todo: Notify user
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
    }
}
