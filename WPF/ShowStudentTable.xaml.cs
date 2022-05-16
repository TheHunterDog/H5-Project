﻿using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Database.Model;
using System;
using System.Diagnostics;

namespace WPF;
public partial class ShowStudentTable : Window

{
    public List<Student> Students { get; set; }
    public ShowStudentTable()
    {
        InitializeComponent();

        using (StudentBeleidContext context = new StudentBeleidContext())
        {
            Students = context.Students.ToList();
        }

        Student.ItemsSource = Students;
    }

    private void selectRow(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        Detailscreen detailscreen = new Detailscreen();
        Student selectedstudent = (Student)Student.SelectedItem;
        detailscreen.studentnr = selectedstudent.Studentnummer;
        detailscreen.addStudentInfo();
        detailscreen.Show();

    }
}