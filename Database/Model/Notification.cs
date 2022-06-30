namespace Database.Model;

public class Notification
{
    #region PrivateFields
    /// <summary>
    /// Notification has an id
    /// </summary>
    private int _id;

    /// <summary>
    /// Notification has a receiver
    /// </summary>
    private StudentSupervisor _receiver;
    
    /// <summary>
    /// Notification has a receiverId
    /// </summary>
    private int _receiverId;
    
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

    public StudentSupervisor Receiver
    {
        get => _receiver;
        set => _receiver = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string Description
    {
        get => _description;
        set => _description = value ?? throw new ArgumentNullException(nameof(value));
    }

    public int ReceiverId
    {
        get => _receiverId;
        set => _receiverId = value;
    }

    #endregion

}
