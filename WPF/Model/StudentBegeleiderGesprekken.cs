using System;

namespace WPF.Model;

public class StudentBegeleiderGesprekken
{
    #region PrivateFields
 /// <summary>
    /// A meeting has a student forgeinkey
    /// </summary>
    private int student_id;
    /// <summary>
    /// A meeting has a student
    /// </summary>
    private Student student;
    /// <summary>
    /// A meeting has a StudentBegeleider Forgeinkey
    /// </summary>
    private int studentBegeleider_id;
    /// <summary>
    /// A meeting has a studentbegeleider
    /// </summary>
    private StudentBegeleider studentBegeleider;
    /// <summary>
    /// A meeting has a date
    /// </summary>
    private DateTime gesprek_datum;
    /// <summary>
    /// A meeting can be completed
    /// </summary>
    private bool voltooid;
    /// <summary>
    /// A meeting may havve notes
    /// </summary>
    private string opmerkingen;
    #endregion
    
    #region Properties

    public int StudentBegeleiderId
    {
        get => studentBegeleider_id;
        set => studentBegeleider_id = value;
    }

    public StudentBegeleider StudentBegeleider
    {
        get => studentBegeleider;
        set => studentBegeleider = value ?? throw new ArgumentNullException(nameof(value));
    }

    public int StudentId
    {
        get => student_id;
        set => student_id = value;
    }

    public Student Student
    {
        get => student;
        set => student = value ?? throw new ArgumentNullException(nameof(value));
    }

    public DateTime GesprekDatum
    {
        get => gesprek_datum;
        set => gesprek_datum = value;
    }

    public bool Voltooid
    {
        get => voltooid;
        set => voltooid = value;
    }

    public string Opmerkingen
    {
        get => opmerkingen;
        set => opmerkingen = value ?? throw new ArgumentNullException(nameof(value));
    }
    #endregion
}