using System;
using System.Windows;
using System.Diagnostics;
using System.Linq; // kan weg
using System.Net.Mail;
using Database.Model;


namespace WPF
{
    /// <summary>
    /// Interaction logic for Inplannen.xaml
    /// </summary>
    public partial class Inplannen : Window
    {
        public Inplannen()
        {
            InitializeComponent();
            DatePicked.BlackoutDates.AddDatesInPast();
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            MailMessage message = new MailMessage("afspraakplanner123@gmail.com", "s1147577@student.windesheim.nl", $"afspraak op {DatePicked.Text}", $"Er is een afspraak ingpepland voor {DatePicked.Text} met als opmerking:\n {opmerkingen.Text}"); // TODO replace email with the students email
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.Credentials = new System.Net.NetworkCredential("afspraakplanner123@gmail.com", "Mailtest123");
            client.EnableSsl = true;
            DateTime datumAfspraak = (DateTime)DatePicked.SelectedDate;
            datumAfspraak = new DateTime(datumAfspraak.Year, datumAfspraak.Month, datumAfspraak.Day, Int16.Parse(Hours.Text), Int16.Parse(Minutes.Text), 0);
            Trace.WriteLine($"{datumAfspraak} + {opmerkingen.Text}");
            //client.Send(message);
            using (var context = new StudentBeleidContext())
            {
                StudentBegeleiderGesprekken gesprek = new StudentBegeleiderGesprekken
                {
                    StudentId = 93,
                    Student = context.Students.Find(93), // TODO replace with the selected student
                    StudentBegeleiderId = 1,
                    StudentBegeleider = context.StudentBegeleiders.Find(71), // TODO replace wit the selected studentbegeleider
                    GesprekDatum = datumAfspraak,
                    Voltooid = false,
                    Opmerkingen = $"{opmerkingen.Text}"
                };
                if (context.StudentBegeleiderGesprekken.Any(a=> a.GesprekDatum == datumAfspraak && a.StudentId == gesprek.StudentId))
                {
                    MessageBox.Show("Afspraak bestaat al!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                context.StudentBegeleiderGesprekken.Add(gesprek);
                context.SaveChanges();
            }
            Close();
        }
    }
}
