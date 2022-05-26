using System.Windows;
using Database.Model;

namespace WPF.Pages;

public partial class ChangePresenceTable : Window
{
    public ChangePresenceTable()
    {
        InitializeComponent();
        
        
        using(var context = new StudentBeleidContext())
        {
            
        }
    }
    
    
}