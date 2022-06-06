

namespace Database.Model;

public class Subject
{
    #region PrivateFields
    /// <summary>
    /// subject has an id
    /// </summary>
    private int _id;
    
    /// <summary>
    /// student has an studentnumber
    /// </summary>
    private string _name;
    
    /// <summary>
    /// Student has a firstname
    /// </summary>
    private string _description;
    
    private Presence[] _presences;

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

    public Presence[] Presences
    {
        get => _presences;
        set => _presences = value ?? throw new ArgumentNullException(nameof(value));
    }

    #endregion
    
}
