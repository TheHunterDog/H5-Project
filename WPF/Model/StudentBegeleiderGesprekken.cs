using System;

namespace WPF.Model;

public class StudentBegeleiderGesprekken
{
    private int student_id;
    private Student student;
    private DateTime gesprek_datum;
    private bool voltooid;
    private string opmerkingen;

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
}