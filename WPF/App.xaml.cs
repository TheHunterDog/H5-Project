#region

using System.Windows;
using Database.Model;

#endregion

namespace WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static DatabaseContext? Context;

        public static bool CreateDatabaseConntection()
        {
            if (DatabaseContext.PingServer())
            {
                Context = new DatabaseContext();
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void DisposeDatabaseConnection()
        {
            Context?.Dispose();
        }
        
        
    }
}
