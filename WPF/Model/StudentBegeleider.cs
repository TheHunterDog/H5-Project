using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace WPF.Model;

public class StudentBegeleider
{
    
    #region PrivateFields
    private int id;
    private string naam;
    private string docentcode;
    private IEnumerable<Student> students;
    private IEnumerable<StudentBegeleiderGesprekken> studentBegeleiderGesprekken;
    #endregion

    #region Properties

    public IEnumerable<StudentBegeleiderGesprekken> StudentBegeleiderGesprekken
    {
        get => studentBegeleiderGesprekken;
        set => studentBegeleiderGesprekken = value ?? throw new ArgumentNullException(nameof(value));
    }

    public int Id
    {
        get => id;
        set => id = value;
    }

    public string Naam
    {
        get => naam;
        set => naam = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string Docentcode
    {
        get => docentcode;
        set => docentcode = value ?? throw new ArgumentNullException(nameof(value));
    }

    public IEnumerable<Student> Students
    {
        get => students;
        set => students = value ?? throw new ArgumentNullException(nameof(value));
    }
    #endregion
}