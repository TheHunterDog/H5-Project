using Microsoft.EntityFrameworkCore;

namespace Database.Model;

public partial class Student
{
    /// <summary>
    /// Student has an id
    /// </summary>
    private int _id;

    /// <summary>
    /// student has an studentNumber
    /// </summary>
    private string _studentNumber;

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
    /// Student has a supervisor ForeignKey
    /// </summary>
    private int _studentSupervisorId;

    /// <summary>
    /// Student has a <see cref="Supervisor"/>
    /// </summary>
    private StudentSupervisor _studentSupervisor;

    /// <summary>
    /// A student belongs to many or zero <see cref="SupervisorMeetings"/>
    /// </summary>
    private IList<StudentSupervisorMeeting> _supervisorMeetings;


    private IList<LearningGoal> _learningGoals;


    /// <summary>
    /// A student belongs to many or zero <see cref="StudentProblem"/>
    /// </summary>
    private IList<StudentProblem> _studentProblems;

    /// <summary>
    /// A student belongs to many or zero <see cref="Subject"/>
    /// </summary>
    private IList<Subject> _subjects;

    public int Id
    {
        get => _id;
        set => _id = value;
    }

    public string StudentNumber
    {
        get => _studentNumber;
        set => _studentNumber = value ?? throw new ArgumentNullException(nameof(value));
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
        get
        {
            DatabaseContext context = new DatabaseContext();
            return context.StudentSupervisor.FirstOrDefault(x => x.Id == _studentSupervisorId) ?? Supervisor;
        }
        set => _studentSupervisor = value;
    }

    public int StudentSupervisor
    {
        get => _studentSupervisorId;
        set => _studentSupervisorId = value;
    }

    public virtual IList<StudentSupervisorMeeting> SupervisorMeetings
    {
        get => _supervisorMeetings;
        set => _supervisorMeetings = value ?? throw new ArgumentNullException(nameof(value));
    }

    public virtual IList<StudentProblem> StudentProblems
    {
        get => _studentProblems;
        set => _studentProblems = value ?? throw new ArgumentNullException(nameof(value));
    }

    public virtual IList<LearningGoal> LearningGoals
    {
        get => _learningGoals;
        set => _learningGoals = value ?? throw new ArgumentNullException(nameof(value));
    }

    public virtual IList<Subject> Subjects
    {
        get => _subjects;
        set => _subjects = value ?? throw new ArgumentNullException(nameof(value));
    }
}