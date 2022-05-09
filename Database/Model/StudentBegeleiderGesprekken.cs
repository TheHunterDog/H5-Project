namespace Database.Model;

public class StudentBegeleiderGesprekken
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
    /// A meeting has a StudentBegeleider Forgeinkey
    /// </summary>
    private int _studentBegeleiderId;
    
    /// <summary>
    /// A meeting has a studentbegeleider
    /// </summary>
    private StudentBegeleider _studentBegeleider;
    
    /// <summary>
    /// A meeting has a date
    /// </summary>
    private DateTime _gesprekDatum;
    
    /// <summary>
    /// A meeting can be completed
    /// </summary>
    private bool _voltooid;
    
    /// <summary>
    /// A meeting may havve notes
    /// </summary>
    private string _opmerkingen;
    
    #endregion
    
    #region Properties

    public int StudentBegeleiderId
    {
        get => _studentBegeleiderId;
        set => _studentBegeleiderId = value;
    }

    public StudentBegeleider StudentBegeleider
    {
        get => _studentBegeleider;
        set => _studentBegeleider = value ?? throw new ArgumentNullException(nameof(value));
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

    public DateTime GesprekDatum
    {
        get => _gesprekDatum;
        set => _gesprekDatum = value;
    }

    public bool Voltooid
    {
        get => _voltooid;
        set => _voltooid = value;
    }

    public string Opmerkingen
    {
        get => _opmerkingen;
        set => _opmerkingen = value ?? throw new ArgumentNullException(nameof(value));
    }
    #endregion
}