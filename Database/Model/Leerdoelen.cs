namespace Database.Model;

public class Leerdoel
{
    #region PrivateFields
    /// <summary>
    /// Leerdoel has an id
    /// </summary>
    private int _id;
    
    /// <summary>
    /// Student has a firstname
    /// </summary>
    private string _beschrijving;
    
    /// <summary>
    /// A Leerdoel belongs to one student
    /// </summary>
    private Student _student;

    private int _studentId;
    #endregion

    #region Properties

    public int Id
    {
        get => _id;
        set => _id = value;
    }

    public string Beschrijving
    {
        get => _beschrijving;
        set => _beschrijving = value ?? throw new ArgumentNullException(nameof(value));
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

    #endregion
    
}
