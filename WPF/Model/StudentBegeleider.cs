using System;
using System.Collections.Generic;

namespace WPF.Model;

public class StudentBegeleider
{
    
    #region PrivateFields
   /// <summary>
    /// studentbegeleider has an id
    /// </summary>
   private int _id;
   
    /// <summary>
    /// Studentbegeleider has a name
    /// </summary>
    private string _naam;
    
    /// <summary>
    /// StudentBegeleider has a docentcode
    /// </summary>
    private string _docentcode;
    
    /// <summary>
    /// StudentBegeleider belongs to many students
    /// </summary>
    private IEnumerable<Student> _students;
    
    /// <summary>
    /// Studentbegeleider belongs to many meetings
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

    public string Naam
    {
        get => _naam;
        set => _naam = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string Docentcode
    {
        get => _docentcode;
        set => _docentcode = value ?? throw new ArgumentNullException(nameof(value));
    }

    public IEnumerable<Student> Students
    {
        get => _students;
        set => _students = value ?? throw new ArgumentNullException(nameof(value));
    }
    #endregion
}
