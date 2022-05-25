namespace Database.Model;

public class Presence
{
        #region PrivateFields
    /// <summary>
    /// Presence has an id
    /// </summary>
    private int _id;
    
    /// <summary>
    /// Presence has an Student
    /// </summary>
    private Student _student;
    
    /// <summary>
    /// Presence has an Student
    /// </summary>
    private int _studentId;
    
    /// <summary>
    /// Presence has a Subject
    /// </summary>
    private Subject _subject;
    
    /// <summary>
    /// Presence has a Subject
    /// </summary>
    private int _subjectId;

    /// <summary>
    /// Presence has a Present to know whether the student was present or not. 
    /// </summary>
    private bool _present;
    
    #endregion

    #region Properties

    public int Id
    {
        get => _id;
        set => _id = value;
    }
    
    

    public Student Student
    {
        get => _student;
        set => _student = value ?? throw new ArgumentNullException(nameof(value));
    }

    public int StudentId
    {
        get => _studentId;
        set => _studentId = value;
    }

    public Subject Subject
    {
        get => _subject;
        set => _subject = value ?? throw new ArgumentNullException(nameof(value));
    }

    public int SubjectId
    {
        get => _subjectId;
        set => _subjectId = value;
    }

    public bool Present
    {
        get => _present;
        set => _present = value;
    }
    
    #endregion

}