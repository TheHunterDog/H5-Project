namespace Database.Model;

public class Teacher : IAuthenticatable
{
    #region PrivateFields

    /// <summary>
    /// Teacher has a name
    /// </summary>
    private readonly string _name;
    
    /// <summary>
    /// A teacher belongs to many or zero <see cref="StudentProblem"/>
    /// </summary>
    private IEnumerable<StudentProblem>? _studentProblems;

    private string _username;
    private string _password;

    #endregion

    public Teacher(string username = "", string password = "", string name = "", string teacherCode = "")
    {
        _username = username;
        _password = password;
        _name = name;
        TeacherCode = teacherCode;
    }
    
    #region Properties

    /// <summary>
    /// Teacher has an id
    /// </summary>
    public int Id { get; set; }

    public string? Name
    {
        get => _name;
        init => _name = value ?? throw new ArgumentNullException(nameof(value));
    }

    public IEnumerable<StudentProblem>? StudentProblems
    {
        get => _studentProblems;
        set => _studentProblems = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string Username
    {
        get => _username;
        set => _username = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string Password
    {
        get => _password;
        set => _password = value ?? throw new ArgumentNullException(nameof(value));
    }

    public bool IsAdmin { get; set; }


    public string TeacherCode { get; init; }

    #endregion
}
