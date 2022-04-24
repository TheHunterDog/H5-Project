using System;
using System.Collections.Generic;
using WPF.Model;

namespace WPF.ViewModel;

public class StudentViewModel
{
    private IEnumerable<Student> _students;
    
    public StudentViewModel()
    {
        /*Real dummy data*/
        _students = new List<Student>
        {
            new Student
            {
                Klasscode = "OOSDDH2022", Voornaam = "Mark", Achternaam = "Heijnekamp", Tussenvoegsel = "",
                Studentnummer = "s1156618", Studentbegeleider = null,StudentbegeleiderId = 0
            },
            new Student
            {
                 Klasscode = "OOSDDH2022", Voornaam = "Rob", Achternaam = "Hutten", Tussenvoegsel = "",
                Studentnummer = "s1152882", Studentbegeleider = null,StudentbegeleiderId = 0
            },            new Student
            {
                 Klasscode = "OOSDDH2022", Voornaam = "Antoine", Achternaam = "Pijlgroms", Tussenvoegsel = "",
                Studentnummer = "s1159362", Studentbegeleider = null,StudentbegeleiderId = 0
            },            new Student
            {
                 Klasscode = "OOSDDH2022", Voornaam = "Evert-Jan", Achternaam = "Nijsink", Tussenvoegsel = "",
                Studentnummer = "s1160918", Studentbegeleider = null,StudentbegeleiderId = 0
            },            new Student
            {
                 Klasscode = "OOSDDH2022", Voornaam = "Tristan", Achternaam = "Jongedijk", Tussenvoegsel = "",
                Studentnummer = "s1147577", Studentbegeleider = null,StudentbegeleiderId = 0
            }
        };
    }

    #region Properties

    public IEnumerable<Student> Students
    {
        get => _students;
        set => _students = value ?? throw new ArgumentNullException(nameof(value));
    }

    #endregion
    
    
}