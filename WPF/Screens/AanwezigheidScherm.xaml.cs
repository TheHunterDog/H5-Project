using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Database.Model;
using WPF.Pages;

namespace WPF.Screens;

public partial class AanwezigheidScherm : Window
{
    /*private DataGridCheckBoxColumn checkBoxColumn;*/
    public AanwezigheidScherm()
    {
        ChangePresenceTable cpt = new ChangePresenceTable();
        InitializeComponent();
        
        cpt.showPresenceTable();
        /*var checkBoxColumn = new DataGridCheckBoxColumn { Header = "Aanwezig", };
        checkBoxColumn.Binding = new Binding("present");
        cpt.PresenceTable.Columns.Add(checkBoxColumn);*/

        PresenceFrame.NavigationService.Navigate(cpt);
    }

  

    private void SavePresenceBtn(object sender, RoutedEventArgs e)
    {
        using (var context = new StudentBeleidContext())
        {
            context.SaveChanges();
        }
    }
    
    private void ClosePresenceBtn(object sender, RoutedEventArgs e)
    {
        Close();
    }
    
}