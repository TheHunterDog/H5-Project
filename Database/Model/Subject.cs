namespace Database.Model;

public class Subject
{
    #region PrivateFields
    /// <summary>
    /// Subject has an id
    /// </summary>
    private int _id;
    
    /// <summary>
    /// Subject has an name
    /// </summary>
    private string _name;
    
    /// <summary>
    /// Subject has a description
    /// </summary>
    private string _description;
    
    /// <summary>
    /// Subject has ec
    /// </summary>
    private string _ec;

    /// <summary>
    /// Subject has amount of lessons
    /// </summary>
    private int _lessons;

    private string _subjectCode;
    
    private IEnumerable<Student> _students;
    #endregion

    #region Properties

    public int Id
    {
        get => _id;
        set => _id = value;
    }

    public string Name
    {
        get => _name;
        set => _name = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string Description
    {
        get => _description;
        set => _description = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string Ec
    {
        get => _ec;
        set => _ec = value ?? throw new ArgumentNullException(nameof(value));
    }

    public int Lessons
    {
        get => _lessons;
        set => _lessons = value;
    }

    public IEnumerable<Student> Students
    {
        get => _students;
        set => _students = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string SubjectCode
    {
        get => _subjectCode;
        set => _subjectCode = value ?? throw new ArgumentNullException(nameof(value));
    }

    #endregion


    public override string ToString()
    {
        return $"{Id}, {Name}, {Description}, {SubjectCode}, {Ec}, {Lessons}";
    }
}
