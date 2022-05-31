using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Database.Model;

namespace WPF.Screens;

public partial class AanwezigheidScherm : Window
{
    public AanwezigheidScherm()
    {
        InitializeComponent();
    }

    private void showPresenceTable()
    {
        using (var context = new StudentBeleidContext())
        {
            var presence = context.Students.ToList();
            PresenceTable.ItemsSource = presence;
            
        }
        
    }

    private void PresenceboxChecked(object sender, RoutedEventArgs e)
    {
        bool aanwezig = AanwezigBox.IsChecked == true;

        if (aanwezig)
        {
            // Zet de kolom Present van 0 naar 1 in de database

            using (var context = new StudentBeleidContext())
            {
            }
        }

        if (!aanwezig)
        {
            // Zet de kolom Present van 1 naar 0 in de database
        }
    }

    private void SavePresenceBtn(object sender, RoutedEventArgs e)
    {
        
    }
    
    private void ClosePresenceBtn(object sender, RoutedEventArgs e)
    {
        
    }
    
}