using System;
using System.Collections.Generic;

namespace WPF.Model;

public class Teacher
{
    #region PrivateFields
    /// <summary>
    /// Teacher has an id
    /// </summary>
    private int _teacherId;
    
    /// <summary>
    /// A teacher belongs to many or zero <see cref="StudentProblem"/>
    /// </summary>
    private IEnumerable<StudentProblem> _studentProblems;
    
    #endregion

    #region Properties

    public int Id
    {
        get => _teacherId;
        set => _teacherId = value;
    }
    
    
    public IEnumerable<StudentProblem> StudentProblems
    {
        get => _studentProblems;
        set => _studentProblems = value ?? throw new ArgumentNullException(nameof(value));
    }
    
    #endregion
}
