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
           
            // checks the id & selects the row of the datagrid
            var id = context.Presences.First();
            var result = context.Presences.FirstOrDefault(x => x.StudentId == id.StudentId);

            // if the row exists change it 
            if (result != null )
            {
                result.StudentId = id.StudentId;
                result.SubjectId = id.SubjectId;
                result.Present = true;
                
                context.Presences.Update(result);
            }
            context.SaveChanges();
        }
    }
    
    private void AanwezigBox_Unchecked(object sender, RoutedEventArgs e)
    {
        using (var context = new StudentBeleidContext())
        {
            // checks the id & selects the row of the datagrid
            var id = context.Presences.First();
            var result = context.Presences.FirstOrDefault(x => x.StudentId == id.StudentId);

            // if the row exists change it 
            if (result != null)
            {
                result.StudentId = id.StudentId;
                result.SubjectId = id.SubjectId;
                result.Present = false;
                
                context.Presences.Update(result);
            }
            context.SaveChanges();
        }
    }
}