#region

using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using Database;
using Database.Model;

#endregion

namespace WPF;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow
{
    private IAuthenticatable _user;
    private bool _loaded = false;
    private static ScaleEngine _scaleEngine;
    public static ScaleEngine GetScaleEngineInstance()
    {
        return _scaleEngine;
    }

    // Main constructor
    public MainWindow()
    {
        _scaleEngine = new ScaleEngine();
        InitializeComponent();
        _user = new DatabaseContext().Teacher.First();
    }


    public MainWindow(IAuthenticatable user) : this()
    {
        _user = user;
    }

    /**
     * <summary>Changes the loaded page on the MainWindow.</summary>
     * <remarks>Uses the x:Name attribute of the calling object to determine which Page to load.</remarks>
     */
    private void ChangeActiveScreen(object sender, RoutedEventArgs e)
    {
        Button caller = (Button) sender;
        
        var fileName = caller.Name;
        var filePath = $".\\Pages\\{fileName}.xaml".ToString();
        Uri fileUri = new Uri(filePath, UriKind.RelativeOrAbsolute);
        ActiveFrame.Source = fileUri;
    }
    
    /**
     * <summary>When app is loaded set initial sizes.</summary>
     */
    private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
    {
        MainWindowObj.MinHeight = _scaleEngine.WindowHeight;
        MainWindowObj.MinWidth = _scaleEngine.WindowWidth;

        MainWindowObj.Height = _scaleEngine.MinHeight;
        MainWindowObj.Width = _scaleEngine.MinWidth;
        
        _loaded = true;
    }


    /**
     * <summary>Subscribe to the ScaleEngine Event.</summary>
     */
    private void MainWindow_OnSizeChanged(object sender, SizeChangedEventArgs e)
    {
        if (_loaded)
            _scaleEngine.OnWindowSizeChange(sender, e);
    }
}