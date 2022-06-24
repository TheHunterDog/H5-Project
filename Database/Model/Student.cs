namespace Database.Model;

public class Student
{
    #region PrivateFields
    /// <summary>
    /// Student has an id
    /// </summary>
    private int _id;
    
    /// <summary>
    /// student has an studentnumber
    /// </summary>
    private string _studentnummer;
    
    /// <summary>
    /// Student has a firstname
    /// </summary>
    private string _firstName;
    
    /// <summary>
    /// student might have a surname prefix
    /// </summary>
    private string _middleName;
    
    /// <summary>
    /// Student has a lastname
    /// </summary>
    private string _lastName;
    
    /// <summary>
    /// student has a class code
    /// </summary>
    private string _classCode;
    
    /// <summary>
    /// Student has a Supervisor forgein-key
    /// </summary>
    private int _studentSupervisorId;
    /// <summary>
    /// Student has a <see cref="Supervisor"/>
    /// </summary>
    private StudentSupervisor _studentSupervisor;
    
    /// <summary>
    /// A student belongs to many or zero <see cref="SupervisorMeetings"/>
    /// </summary>
    private IEnumerable<StudentSupervisorMeeting> _supervisorMeetings;


    private IEnumerable<LearningGoal> _learningGoals;

    
    /// <summary>
    /// A student belongs to many or zero <see cref="StudentProblem"/>
    /// </summary>
    private IEnumerable<StudentProblem> _studentProblems;
    
    /// <summary>
    /// A student belongs to many or zero <see cref="Subject"/>
    /// </summary>
    private ICollection<Subject> _subjects;

    
    #endregion

    #region Properties

    public int Id
    {
        get => _id;
        set => _id = value;
    }

    public string Studentnummer
    {
        get => _studentnummer;
        set => _studentnummer = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string FirstName
    {
        get => _firstName;
        set => _firstName = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string? MiddleName
    {
        get => _middleName;
        set => _middleName = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string LastName
    {
        get => _lastName;
        set => _lastName = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string ClassCode
    {
        get => _classCode;
        set => _classCode = value ?? throw new ArgumentNullException(nameof(value));
    }

    public virtual StudentSupervisor Supervisor
    {
        get => _studentSupervisor;
        set => _studentSupervisor = value;
    }

    public int StudentSupervisor
    {
        get => _studentSupervisorId;
        set => _studentSupervisorId = value;
    }
    
    public virtual IEnumerable<StudentSupervisorMeeting> SupervisorMeetings
    {
        get => _supervisorMeetings;
        set => _supervisorMeetings = value ?? throw new ArgumentNullException(nameof(value));
    }
    
    public virtual IEnumerable<StudentProblem> StudentProblems
    {
        get => _studentProblems;
        set => _studentProblems = value ?? throw new ArgumentNullException(nameof(value));
    }

    public virtual IEnumerable<LearningGoal> LearningGoals
    {
        get => _learningGoals;
        set => _learningGoals = value ?? throw new ArgumentNullException(nameof(value));
    }

    public ICollection<Subject> Subjects
    {
        get => _subjects;
        set => _subjects = value ?? throw new ArgumentNullException(nameof(value));
    }

    #endregion

    public override string ToString()
    {
        return $"{Id}, {FirstName}, {LastName}, {Studentnummer}, {StudentSupervisor}";
    }

    public override bool Equals(object? obj)
    {
        return ((Student)obj).Id == Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}
