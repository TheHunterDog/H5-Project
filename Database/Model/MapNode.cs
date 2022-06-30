using Microsoft.Maps.MapControl.WPF;

namespace Database.Model;

public class MapNode
{
    #region PrivateFields
    /// <summary>
    /// map node has an id
    /// </summary>
    private int _id;

    private LocationCollection _locationCollection;
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

    public string Letter
    {
        get => _letter;
        set => _letter = value ?? throw new ArgumentNullException(nameof(value));
    }
    public LocationCollection LocationCollection
    {
        get => _locationCollection;
        set => _locationCollection = value ?? throw new ArgumentNullException(nameof(value));
    }

    #endregion
    
}
