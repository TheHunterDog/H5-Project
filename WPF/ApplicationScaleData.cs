namespace WPF;

public class ApplicationScaleData
{
    /**
     * <summary>Minimum width the application should have at any moment.</summary>
     */
    public double MinWidth = 640;
    
    /**
     * <summary>Minimum height the application should have at any moment.</summary>
     */
    public double MinHeight = 480;
    
    
    /**
     * <summary>Holds the current window width.</summary>
     */
    private double _windowWidth = 640;
    /**
     * <summary>Holds the current window height.</summary>
     */
    private double _windowHeight = 480;

    
    /**
     * <summary>Gets the current window width.</summary>
     */
    public double WindowWidth
    {
        get => _windowWidth;
        protected set => _windowWidth = value >= 320 ? value : 320;
    }
    
    
    /**
     * <summary>Gets the current window height.</summary>
     */
    public double WindowHeight
    {
        get => _windowHeight;
        protected set => _windowHeight = value >= 240 ? value : 240;
    }
}