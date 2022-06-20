namespace Database.Model;

public class Notification
{
    #region PrivateFields
    /// <summary>
    /// Notification has an id
    /// </summary>
    private int _id;
    
    /// <summary>
    /// Notification has an sender
    /// </summary>
    private IAuthenticatable _sender;
    
    /// <summary>
    /// Notification has a receiver
    /// </summary>
    private IAuthenticatable _receiver;
    
    /// <summary>
    /// Notification has a Description
    /// </summary>
    private string _description;
    #endregion

    #region properties

    public int Id
    {
        get => _id;
        set => _id = value;
    }

    public IAuthenticatable Sender
    {
        get => _sender;
        set => _sender = value ?? throw new ArgumentNullException(nameof(value));
    }

    public IAuthenticatable Receiver
    {
        get => _receiver;
        set => _receiver = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string Description
    {
        get => _description;
        set => _description = value ?? throw new ArgumentNullException(nameof(value));
    }

    #endregion

}
