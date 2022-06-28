using Microsoft.Maps.MapControl.WPF;

namespace Database.Model;

public class MapNode
{
    #region PrivateFields
    /// <summary>
    /// map node has an id
    /// </summary>
    private int _id;
    
    /// <summary>
    /// map node has longitude
    /// </summary>
    private double _longitude;
    
    /// <summary>
    /// map node has latitude
    /// </summary>
    private double _latitude;

    private LocationCollection _locationCollection;
    /// <summary>
    /// map node has a letter
    /// </summary>
    private string _letter;

    private double paddingVertical;
    private double paddingHorizontal;

    #endregion

    #region Properties

    public int Id
    {
        get => _id;
        set => _id = value;
    }

    public double Longitude
    {
        get => _longitude;
        set => _longitude = value;
    }

    public double Latitude
    {
        get => _latitude;
        set => _latitude = value;
    }

    public string Letter
    {
        get => _letter;
        set => _letter = value ?? throw new ArgumentNullException(nameof(value));
    }

    public double PaddingVertical
    {
        get => paddingVertical;
        set => paddingVertical = value;
    }

    public double PaddingHorizontal
    {
        get => paddingHorizontal;
        set => paddingHorizontal = value;
    }

    public LocationCollection LocationCollection
    {
        get => _locationCollection;
        set => _locationCollection = value ?? throw new ArgumentNullException(nameof(value));
    }

    #endregion
    
}
