namespace Database.Model;

public class MapNodes
{
    #region PrivateFields
    /// <summary>
    /// map node has an id
    /// </summary>
    private int _id;
    
    /// <summary>
    /// map node has longitude
    /// </summary>
    private int _longitude;
    
    /// <summary>
    /// map node has latitude
    /// </summary>
    private string _latitude;
    
    /// <summary>
    /// map node has a description
    /// </summary>
    private string _description;
    
    /// <summary>
    /// map node has a letter
    /// </summary>
    private string _letter;

    #endregion

    #region Properties

    public int Id
    {
        get => _id;
        set => _id = value;
    }

    public int Longitude
    {
        get => _longitude;
        set => _longitude = value;
    }

    public string Latitude
    {
        get => _latitude;
        set => _latitude = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string Description
    {
        get => _description;
        set => _description = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string Letter
    {
        get => _letter;
        set => _letter = value ?? throw new ArgumentNullException(nameof(value));
    }

    #endregion
    
}
