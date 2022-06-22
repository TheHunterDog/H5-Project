namespace Database.Model;

public class StudentBegeleider: IAuthenticatable
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
    
    private String _username;
    private string _password;
    private bool _isAdmin;
    
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

    public IEnumerable<Notification> Notifications { get; set; }

    public IEnumerable<Notification> NotificationsSent { get; set; }
    public IEnumerable<Notification> NotificationsRecived { get; set; }

    #endregion

    public StudentBegeleider()
    {
        _naam = "";
        _docentcode = "";
        this.Naam = "";
        this.Docentcode = "";
    }

    public override string ToString()
    {
        return $"{Id}, {Naam}, {Docentcode}";
    }
}
