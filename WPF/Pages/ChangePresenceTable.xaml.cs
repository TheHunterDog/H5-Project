using System;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Database.Model;


namespace WPF.Pages;

public partial class ChangePresenceTable : Page
{
    public Presence SelectedPresence;
    public ChangePresenceTable()
    {
        InitializeComponent();
        
    }
    public void showPresenceTable()
    {
        using (var context = new StudentBeleidContext())
        {
            var presence = context.Presences.ToList();
            PresenceTable.ItemsSource = presence;
        }
        
    }

    private void AanwezigBox_Checked(object sender, RoutedEventArgs e)
    {
        using (var context = new StudentBeleidContext())
        {
            SelectedPresence = (Presence)PresenceTable.SelectedItem;

            // if the id doesn't exist make a new object
            if (context.Presences.Any(x => x.StudentId == SelectedPresence.StudentId))
            {
                Presence p = new Presence {StudentId = SelectedPresence.StudentId, SubjectId = SelectedPresence.SubjectId ,Present = false};
                context.Add(p);
                
            }
                // Searches for the correct id in the table if it exists
            var IdSearch = context.Presences.FirstOrDefault(x => x.StudentId == SelectedPresence.StudentId);

            if (IdSearch != null)
            {
                context.Presences.Update(SelectedPresence);
            }
            context.SaveChanges();
        }
        
    }

    private void AanwezigBox_Unchecked(object sender, RoutedEventArgs e)
    {
        using (var context = new StudentBeleidContext())
        {
            SelectedPresence = (Presence)PresenceTable.SelectedItem;

            if (context.Presences.Any(x => x.StudentId == SelectedPresence.StudentId))
            {
                Presence p = new Presence {StudentId = SelectedPresence.StudentId, SubjectId = SelectedPresence.SubjectId ,Present = false};
                context.Add(p);
                context.SaveChanges();
            }

            var IdSearch = context.Presences.FirstOrDefault(x => x.StudentId == SelectedPresence.StudentId);

            if (IdSearch != null)
            {
                context.Presences.Update(SelectedPresence);
            }
            context.SaveChanges();
        }
    }
}