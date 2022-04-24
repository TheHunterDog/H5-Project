using System;

namespace WPF.Model;

public class StudentBegeleiderGesprekken
{
    #region PrivateFields
    private int student_id;
    private Student student;
    private int studentBegeleider_id;
    private StudentBegeleider studentBegeleider;
    private DateTime gesprek_datum;
    private bool voltooid;
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