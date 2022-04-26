﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Diagnostics; // kan weg
using System.Net.Mail;


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
            MailMessage message = new MailMessage("afspraakplanner123@gmail.com", "s1147577@student.windesheim.nl", $"afspraak op {DatePicked.Text}", $"Er is een afspraak ingpepland voor {DatePicked.Text} met als opmerking:\n {opmerkingen.Text}");
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.Credentials = new System.Net.NetworkCredential("afspraakplanner123@gmail.com", "Mailtest123");
            client.EnableSsl = true;
            DateTime datumAfspraak = (DateTime)DatePicked.SelectedDate;
            datumAfspraak = new DateTime(datumAfspraak.Year, datumAfspraak.Month, datumAfspraak.Day, Int16.Parse(Hours.Text), Int16.Parse(Minutes.Text), 0);
            Trace.WriteLine($"{datumAfspraak} + {opmerkingen.Text}");
            //client.Send(message);
            /*            Model.StudentBegeleiderGesprekken test = new Model.StudentBegeleiderGesprekken
                        {
                            GesprekDatum = datumAfspraak,
                            StudentId = 1147577,
                            StudentBegeleider = new Model.StudentBegeleider
                            {
                                Docentcode = "test123",
                                Id = 123,
                                Naam = "peter pottebakker",
                                Students = new List<Model.Student>()
                            },
                            Opmerkingen = $"{opmerkingen.Text}",
                            Student = new Model.Student
                            {

                            },
                            Voltooid = false
                        };*/
            using (var contex = new Model.StudentBeleidContext())
            {
                contex.StudentBegeleiderGesprekken.Add(new Model.StudentBegeleiderGesprekken
                {
                    StudentId = 1,
                    Student = contex.Students.Find(1),
                    StudentBegeleiderId = 1,
                    StudentBegeleider = contex.StudentBegeleiders.Find(1),
                    GesprekDatum = datumAfspraak,
                    Voltooid = false,
                    Opmerkingen = $"{opmerkingen.Text}"
                });
                contex.SaveChanges();
            }
            this.Close();
        }
    }
}