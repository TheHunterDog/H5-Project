namespace Database.Model;

public class StudentSupervisorMeeting
{
    #region PrivateFields
 /// <summary>
    /// A meeting has a student forgeinkey
    /// </summary>
    private int _studentId;
 
    /// <summary>
    /// A meeting has a student
    /// </summary>
    private Student _student;
    
    /// <summary>
    /// A meeting has a StudentSupervisor Forgeinkey
    /// </summary>
    private int _studentSupervisorId;
    
    /// <summary>
    /// A meeting has a studentbegeleider
    /// </summary>
    private StudentSupervisor _studentSupervisor;
    
    /// <summary>
    /// A meeting has a date
    /// </summary>
    private DateTime _meetingDate;
    
    /// <summary>
    /// A meeting can be completed
    /// </summary>
    private bool _done;
    
    /// <summary>
    /// A meeting may have notes
    /// </summary>
    private string _comments;
    
    #endregion
    
    #region Properties

    public int StudentSupervisorId
    {
        get => _studentSupervisorId;
        set => _studentSupervisorId = value;
    }

    public StudentSupervisor StudentSupervisor
    {
        get => _studentSupervisor;
        set => _studentSupervisor = value ?? throw new ArgumentNullException(nameof(value));
    }

    public int StudentId
    {
        get => _studentId;
        set => _studentId = value;
    }

    public Student Student
    {
        get => _student;
        set => _student = value ?? throw new ArgumentNullException(nameof(value));
    }

    public DateTime MeetingDate
    {
        get => _meetingDate;
        set => _meetingDate = value;
    }

    public bool Done
    {
        get => _done;
        set => _done = value;
    }

    public string Comments
    {
        get => _comments;
        set => _comments = value ?? throw new ArgumentNullException(nameof(value));
    }
    #endregion
}