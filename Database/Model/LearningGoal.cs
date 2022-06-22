namespace Database.Model;

public class LearningGoal
{
    #region PrivateFields
    /// <summary>
    /// LearningGoal has an id
    /// </summary>
    private int _id;
    
    /// <summary>
    /// Student learninGoal has a description
    /// </summary>
    private string _description;
    
    /// <summary>
    /// A LearningGoal belongs to one student
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

    public string Description
    {
        get => _description;
        set => _description = value ?? throw new ArgumentNullException(nameof(value));
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
