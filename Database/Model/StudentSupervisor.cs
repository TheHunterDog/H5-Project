namespace Database.Model;

public class StudentSupervisor: IAuthenticatable
{
    
    #region PrivateFields
   /// <summary>
    /// StudentSupervisor has an id
    /// </summary>
   private int _id;
   
    /// <summary>
    /// StudentSupervisor has a name
    /// </summary>
    private string? _name;
    
    /// <summary>
    /// StudentSupervisor has a docentcode
    /// </summary>
    private string _teacherCode;
    
    /// <summary>
    /// StudentSupervisor belongs to many students
    /// </summary>
    private IEnumerable<Student> _students;
    
    /// <summary>
    /// StudentSupervisor belongs to many meetings
    /// </summary>
    private IEnumerable<StudentSupervisorMeeting> _studentSupervisorMeetings;
    
    private String _username;
    private string _password;
    private bool _isAdmin;
    
    #endregion

    #region Properties

    public IEnumerable<StudentSupervisorMeeting> StudentSupervisorMeetings
    {
        get => _studentSupervisorMeetings;
        set => _studentSupervisorMeetings = value ?? throw new ArgumentNullException(nameof(value));
    }

    public int Id
    {
        get => _id;
        set => _id = value;
    }

    public string? Name
    {
        get => _name;
        set => _name = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string TeacherCode
    {
        get => _teacherCode;
        set => _teacherCode = value ?? throw new ArgumentNullException(nameof(value));
    }

    public IEnumerable<Student> Students
    {
        get => _students;
        set => _students = value ?? throw new ArgumentNullException(nameof(value));
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

    public bool IsAdmin
    {
        get => _isAdmin;
        set => _isAdmin = value;
    }

    #endregion

    public StudentSupervisor()
    {
        _name = "";
        _teacherCode = "";
        Name = "";
        TeacherCode = "";
    }

    public override string ToString()
    {
        return $"{Id}, {Name}, {TeacherCode}";
    }
}
