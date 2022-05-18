namespace Database.Model;

public class Teacher
{
    #region PrivateFields
    /// <summary>
    /// Teacher has an id
    /// </summary>
    private int _teacherId;
    
    /// <summary>
    /// Teacher has a name
    /// </summary>
    private string _name;
    
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

    public string Name
    {
        get => _name;
        set => _name = value;
    }

    public IEnumerable<StudentProblem> StudentProblems
    {
        get => _studentProblems;
        set => _studentProblems = value ?? throw new ArgumentNullException(nameof(value));
    }
    
    #endregion
}
