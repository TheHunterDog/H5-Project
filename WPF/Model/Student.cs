using System;
using System.Collections.Generic;
using System.Linq;

namespace WPF.Model;

public class Student
{
    #region PrivateFields
    /// <summary>
    /// Student has an id
    /// </summary>
    private int _id;
    
    /// <summary>
    /// student has an studentnumber
    /// </summary>
    private string _studentnummer;
    
    /// <summary>
    /// Student has a firstname
    /// </summary>
    private string _voornaam;
    
    /// <summary>
    /// student might have a surname prefix
    /// </summary>
    private string _tussenvoegsel;
    
    /// <summary>
    /// Student has a lastname
    /// </summary>
    private string _achternaam;
    
    /// <summary>
    /// student has a class code
    /// </summary>
    private string _klasscode;
    
    /// <summary>
    /// Student has a Studentbegeleider forgein-key
    /// </summary>
    private int _studentbegeleiderId;
    /// <summary>
    /// Student has a studentbegeleider
    /// </summary>
    private StudentBegeleider _studentBegeleider;
    
    /// <summary>
    /// A student belongs to many meetings
    /// </summary>
    private IEnumerable<StudentBegeleiderGesprekken> _studentBegeleiderGesprekken;
    
    #endregion

    #region Properties

    public IEnumerable<StudentBegeleiderGesprekken> StudentBegeleiderGesprekken
    {
        get => _studentBegeleiderGesprekken;
        set => _studentBegeleiderGesprekken = value ?? throw new ArgumentNullException(nameof(value));
    }


    public int Id
    {
        get => _id;
        set => _id = value;
    }

    public string Studentnummer
    {
        get => _studentnummer;
        set => _studentnummer = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string Voornaam
    {
        get => _voornaam;
        set => _voornaam = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string Tussenvoegsel
    {
        get => _tussenvoegsel;
        set => _tussenvoegsel = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string Achternaam
    {
        get => _achternaam;
        set => _achternaam = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string Klasscode
    {
        get => _klasscode;
        set => _klasscode = value ?? throw new ArgumentNullException(nameof(value));
    }

    public StudentBegeleider Studentbegeleider
    {
        get => _studentBegeleider;
        set => _studentBegeleider = value;
    }

    public int StudentbegeleiderId
    {
        get => _studentbegeleiderId;
        set => _studentbegeleiderId = value;
    }

    #endregion
    
}
