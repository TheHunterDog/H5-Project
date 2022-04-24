using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WPF.Model;

public class Student
{
    #region PrivateFields
    private int id;
    private string studentnummer;
    private string voornaam;
    private string tussenvoegsel;
    private string achternaam;
    private string klasscode;
    private int studentbegeleiderId;
    private StudentBegeleider studentBegeleider;
    private IEnumerable<StudentBegeleiderGesprekken> studentBegeleiderGesprekkens;
    #endregion

    #region Properties

    public IEnumerable<StudentBegeleiderGesprekken> StudentBegeleiderGesprekkens
    {
        get => studentBegeleiderGesprekkens;
        set => studentBegeleiderGesprekkens = value ?? throw new ArgumentNullException(nameof(value));
    }


    public int Id
    {
        get => id;
        set
        {
            id = value;
            onProprtyChanged("Id");
        }
    }

    public string Studentnummer
    {
        get => studentnummer;
        set
        {
            studentnummer = value ?? throw new ArgumentNullException(nameof(value));
            onProprtyChanged("Studentnummer");

        }
    }

    public string Voornaam
    {
        get => voornaam;
        set
        {
            voornaam = value ?? throw new ArgumentNullException(nameof(value));
            onProprtyChanged("Voornaam");
        }
    }

    public string Tussenvoegsel
    {
        get => tussenvoegsel;
        set { 
            tussenvoegsel = value ?? throw new ArgumentNullException(nameof(value)); 
            onProprtyChanged("Tussenvoegsel");
        }
}

    public string Achternaam
    {
        get => achternaam;
        set
        {
            achternaam = value ?? throw new ArgumentNullException(nameof(value));
            onProprtyChanged("Achternaam");

        }
    }

    public string Klasscode
    {
        get => klasscode;
        set
        {
            klasscode = value ?? throw new ArgumentNullException(nameof(value));
            onProprtyChanged("Klasscode");
        }
    }

    public StudentBegeleider Studentbegeleider
    {
        get => studentBegeleider;
        set
        {
            studentBegeleider = value;
            onProprtyChanged("Studentbegeleider");
        }
    }

    public int StudentbegeleiderId
    {
        get => studentbegeleiderId;
        set
        {
            studentbegeleiderId = value;
            onProprtyChanged("StudentbegeleiderId");
        }
    }

    #endregion

    #region Propertychanged

    public event PropertyChangedEventHandler? PropertyChanged;

    private void onProprtyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    #endregion
}