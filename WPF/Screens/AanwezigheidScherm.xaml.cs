using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Database.Model;

namespace WPF.Screens;

public partial class AanwezigheidScherm : Window
{
    public Presence SelectedPresence;
    public AanwezigheidScherm()
    {
        InitializeComponent();
    }

    private void showPresenceTable()
    {
        using (var context = new StudentBeleidContext())
        {
            var presence = context.Presences.ToList();
            PresenceTable.ItemsSource = presence;
            
        }
        
    }

    private void PresenceboxChecked(object sender, RoutedEventArgs e)
    {
        bool aanwezig = AanwezigBox.IsChecked == true;

        if (aanwezig)
        {
            // Zet de kolom Present van 0 naar 1 in de database

            Presence p = new Presence { Present = true };
            using (var context = new StudentBeleidContext())
            {
                context.Add(p);
            }
        }

        if (!aanwezig)
        {
            // Zet de kolom Present van 1 naar 0 in de database

            using (var context = new StudentBeleidContext())
            {
              var result = context.Presences.SingleOrDefault(i => i.Id == SelectedPresence.Id);

              if (result == null)
              {
                  Presence p = new Presence { Present = false };
                  context.Add(p);
              }
            }
        }
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