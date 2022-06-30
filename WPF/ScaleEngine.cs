using System.Windows;

namespace WPF;

public class ScaleEngine : ApplicationScaleData
{
    public void ReCalculateNavbarFontSize()
    {
        double controlsize = WindowWidth / 12 / 3 * 2.15 / 5 * 1.8;
        if (controlsize > 25)
        {
            controlsize = 25;
        }

        AddOrEditResources("ControlFontSize", controlsize);
    }


    #region HelperMethods
    
    /**
     * <summary>Set the Window Height.</summary>
     */
    private void SetWindowHeight(double height)
    {
        WindowHeight = height;
    }
    
    /**
     * <summary>Set the Window Width</summary>
     */
    private void SetWindowWidth(double width)
    {
        WindowWidth = width;
    }
    
    /**
     * <summary>Add or Edit a <see cref="FrameworkElement.Resources"/>.</summary>
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
     */
    public virtual void OnWindowSizeChange(object sender, SizeChangedEventArgs e)
    {
        // Check for new Height
        if (e.HeightChanged)
            SetWindowHeight(e.NewSize.Height);
    
        // Check for new Width
        if(e.WidthChanged)
            SetWindowWidth(e.NewSize.Width);

        ReCalculateNavbarFontSize();
    }
    
    #endregion
    

    
    
    
    
    

    
}