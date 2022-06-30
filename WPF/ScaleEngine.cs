using System.Windows;

namespace WPF;

public class ScaleEngine : ApplicationScaleData
{
    /**
     * Recalculate the application wide font size using the current width and height of the application. 
     */
    private void ReCalculateFontSize()
    {
        double controlsize = WindowWidth / 12 / 3 * 2.15 / 5 * 1.2;
        if (controlsize > 22)
        {
            controlsize = 22;
        }

        AddOrEditResources("ControlFontSize", controlsize);
    }


    #region HelperMethods
    
    /**
     * <summary>Sets the Window height.</summary>
     * <param name="height">The new height of the window.</param>
     */
    private void SetWindowHeight(double height)
    {
        WindowHeight = height;
    }
    
    
    /**
     * <summary>Sets the Window width.</summary>
     * <param name="width">The new width of the window.</param>
     */
    private void SetWindowWidth(double width)
    {
        WindowWidth = width;
    }
    
    /**
     * <summary>Add or Edit a <see cref="FrameworkElement.Resources"/>.</summary>
     * <param name="key">The resource key</param>
     * <param name="value">The new resource value</param>
     */
    private void AddOrEditResources(string key, double value)
    {
        // Remove if exists
        if (Application.Current.Resources[key] != null)
            Application.Current.Resources.Remove(key);

        // ReAdd with new value
        Application.Current.Resources.Add(key, value);
    }
    
    #endregion

    #region Events
    
    /**
     * <summary>Dispatch method called from the MainWindow.</summary>
     * <param name="sender">The object calling this method</param>
     * <param name="e">The <see cref="SizeChangedEventArgs"/> arguments</param>
     */
    public virtual void OnWindowSizeChange(object sender, SizeChangedEventArgs e)
    {
        // Check for new height
        if (e.HeightChanged)
            SetWindowHeight(e.NewSize.Height);
    
        // Check for new width
        if(e.WidthChanged)
            SetWindowWidth(e.NewSize.Width);

        // Recalculate the application font size
        ReCalculateFontSize();
    }
    
    public virtual void OnWindowSizeChange(Window sender)
    {
        double height = sender.ActualHeight;
        double width = sender.ActualWidth;
        
        
        // Check for new Height
        if (WindowHeight != height)
            SetWindowHeight(height);
    
        // Check for new Width
        if(WindowWidth != width)
            SetWindowWidth(width);

        ReCalculateFontSize();
    }
    
    #endregion
    

    
    
    
    
    

    
}