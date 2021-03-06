namespace Database.Model;

public class StudentProblem
{
    #region PrivateFields
    /// <summary>
    /// StudentProblem has an ID
    /// </summary>
    private int _id;
    
    /// <summary>
    /// StudentProblem has an associated studentId
    /// </summary>
    private int _studentId;

    /// <summary>
    /// StudentProblem has an associated <see cref="Student"/>
    /// </summary>
    private Student _student;

    /// <summary>
    /// StudentProblem has an associated Teacher
    /// </summary>
    private int _teacherId;

    /// <summary>
    /// StudentProblem has an associated <see cref="Teacher"/>
    /// </summary>
    private Teacher _teacher;
    
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

    public int Id
    {
        get => _id;
        set => _id = value;
    }
    
    public int StudentId
    {
        get => _studentId;
        set => _studentId = value;
    }
    
    public virtual Student Student
    {
        get => _student;
        set => _student = _student = value ?? throw new ArgumentNullException(nameof(value));
    }
    
    public int TeacherId
    {
        get => _teacherId;
        set => _teacherId = value;
    }
    
    public virtual Teacher Teacher
    {
        get => _teacher;
        set => _teacher = _teacher = value ?? throw new ArgumentNullException(nameof(value));
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

    public override string ToString()
    {
        return $"{StudentId}, {Description}, {Priority}, {TeacherId}";
    }
}
