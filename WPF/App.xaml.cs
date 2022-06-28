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
        public static DatabaseContext context = new DatabaseContext();
    }
}
