using System;
using System.Collections.Generic;

namespace WPF.Model;

public class StudentProblem
{
    #region PrivateFields
    /// <summary>
    /// StudentProblem has an associated <see cref="Student"/>
    /// </summary>
    private int _studentId;
    
    /// <summary>
    /// StudentProblem has an associated Teacher
    /// </summary>
    private int _teacherId;
    
    /// <summary>
    /// StudentProblem has an priority
    /// </summary>
    private int _priority;
    
    /// <summary>
    /// StudentProblem has an description
    /// </summary>
    private string _description;
    
    #endregion

    #region Properties

    public int Student
    {
        get => _studentId;
        set => _studentId = value;
    }
    
    public int Teacher
    {
        get => _teacherId;
        set => _teacherId = value;
    }
    
    public int Priority
    {
        get => _priority;
        set => _priority = value;
    }
    
    public string Description
    {
        get => _description;
        set => _description = value;
    }
    
    #endregion
}
