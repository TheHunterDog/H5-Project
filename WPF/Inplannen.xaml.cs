using System;
using System.Linq;
using System.Windows;
using System.Net.Mail;
using Database.Model;


namespace WPF
{
    /// <summary>
    /// Interaction logic for Inplannen.xaml
    /// </summary>
    public partial class Inplannen : Window
    {
        public string studentnr = "";
        public Inplannen()
        {
            InitializeComponent();
            DatePicked.BlackoutDates.AddDatesInPast();
        }
        
        /**
         * <summary>button click logic</summary> 
         */
        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            // get time out of the DatePicker and combibox
            DateTime datumAfspraak = (DateTime)DatePicked.SelectedDate;
            datumAfspraak = new DateTime(datumAfspraak.Year, datumAfspraak.Month, datumAfspraak.Day, Int16.Parse(Hours.Text), Int16.Parse(Minutes.Text), 0);

            // specify the database
            using (var context = new StudentBeleidContext())
            {
                // find the student
                Student selectedstudent = context.Students.Where(x => x.Studentnummer == studentnr).First();
                // make the meeting
                StudentBegeleiderGesprekken meeting = new StudentBegeleiderGesprekken
                {
                    StudentId = selectedstudent.Id,
                    Student = selectedstudent,
                    StudentBegeleiderId = selectedstudent.StudentbegeleiderId,
                    StudentBegeleider = context.StudentBegeleiders.Where(x => x.Id == selectedstudent.StudentbegeleiderId).First(),
                    GesprekDatum = datumAfspraak,
                    Opmerkingen = $"{opmerkingen.Text}"
                };

                // check if the meeting already exists
                if (context.StudentBegeleiderGesprekken.Any(a=> a.GesprekDatum == datumAfspraak && a.StudentId == meeting.StudentId))
                {
                    MessageBox.Show("Afspraak bestaat al!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                //send a mail to the student
                send_Mail(datumAfspraak, opmerkingen.Text, selectedstudent.Studentnummer);
                // save and add the meeting to the database
                context.StudentBegeleiderGesprekken.Add(meeting);
                context.SaveChanges();
            }
            Close();
        }

        /**
         * <summary>  </summary> 
         */
        private void send_Mail(DateTime date, string notes, string studentnr)
        {
            //create the message
            MailMessage message = new MailMessage("afspraakplanner123@gmail.com", $"@{studentnr}@student.windesheim.nl", $"afspraak op {date}", $"Er is een afspraak ingpepland op {date} met als opmerking:\n {notes}"); // TODO replace email with the students email
            //config the mail service
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.Credentials = new System.Net.NetworkCredential("afspraakplanner123@gmail.com", "Mailtest123");
            client.EnableSsl = true;
            //send the email
            client.Send(message);
        }
    }
}
