using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace WPF.Model;

public class StudentBegeleider
{
    
    #region PrivateFields
   /// <summary>
    /// studentbegeleider has an id
    /// </summary>
    private int id;
    /// <summary>
    /// Studentbegeleider has a name
    /// </summary>
    private string naam;
    /// <summary>
    /// StudentBegeleider has a docentcode
    /// </summary>
    private string docentcode;
    /// <summary>
    /// StudentBegeleider belongs to many students
    /// </summary>
    private IEnumerable<Student> students;
    /// <summary>
    /// Studentbegeleider belongs to many meetings
    /// </summary>
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