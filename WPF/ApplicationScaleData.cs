namespace WPF;

public class ApplicationScaleData
{
    private double _windowWidth = 640;
    private double _windowHeight = 480;
    private int _windowFontSize = 12;
    

    private const int SideBarWidth = 42;
    
    public double WindowWidth
    {
        get => _windowWidth;
        protected set => _windowWidth = value >= 320 ? value : 320;
    }
    
    public double WindowHeight
    {
        get => _windowHeight;
        protected set => _windowHeight = value >= 240 ? value : 240;
    }
    
    public int WindowFontSize
    {
        get => _windowFontSize;
        protected set => _windowFontSize = value;
    }
    
    
    
    #region FontSizes
    
    // FontSizes
    private int _navbarFontSize = 24;
    public int NavbarFontSize { get => _navbarFontSize; set => _navbarFontSize = value; }
    
    #endregion
}