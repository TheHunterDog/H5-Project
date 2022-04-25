using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WPF.Model;

public class Student
{
    #region PrivateFields
    


    /// <summary>
    /// Student has an id
    /// </summary>
    private int id;
    /// <summary>
    /// student has an studentnumber
    /// </summary>
    private string studentnummer;
    /// <summary>
    /// Student has a firstname
    /// </summary>
    private string voornaam;
    /// <summary>
    /// student might have a surname prefix
    /// </summary>
    private string tussenvoegsel;
    /// <summary>
    /// Student has a lastname
    /// </summary>
    private string achternaam;
    /// <summary>
    /// student has a class code
    /// </summary>
    private string klasscode;
    /// <summary>
    /// Student has a Studentbegeleider forgein-key
    /// </summary>
    private int studentbegeleiderId;
    /// <summary>
    /// Student has a studentbegeleider
    /// </summary>
    private StudentBegeleider studentBegeleider;
    /// <summary>
    /// A student belongs to many meetings
    /// </summary>
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