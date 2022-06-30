#region

using System.Linq;
using System.Windows;
using Database;
using Database.Model;

#endregion

namespace WPF;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow
{
    private int _screen;
    private IAuthenticatable _user;
    private bool _loaded = false;

    private ScaleEngine ScaleEngine;

    private bool ShowGridLines = true;

    // Main constructor
    public MainWindow()
    {
        _screen = -1;
        ScaleEngine = new ScaleEngine();
        
        InitializeComponent();


        ShowDebugGridLines();


        _user = new DatabaseContext().Teacher.First();
    }


    public MainWindow(IAuthenticatable user) : this()
    {
        _user = user;
    }

    private void Btn4_OnClick(object sender, RoutedEventArgs e)
    {
        MasterGrid.ShowGridLines = !MasterGrid.ShowGridLines;
        TopGrid.ShowGridLines = !TopGrid.ShowGridLines;
        NavbarGrid.ShowGridLines = !NavbarGrid.ShowGridLines;
        ContentGrid.ShowGridLines = !ContentGrid.ShowGridLines;
    }

    /**
     * <summary>When app is loaded set initial sizes.</summary>
     */
    private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
    {
        MainWindowObj.MinHeight = ScaleEngine.WindowHeight;
        MainWindowObj.MinWidth = ScaleEngine.WindowWidth;

        MainWindowObj.Height = ScaleEngine.WindowHeight;
        MainWindowObj.Width = ScaleEngine.WindowWidth;
        _loaded = true;
    }
    

    /**
     * <summary>Subscribe to the ScaleEngine Event.</summary>
     */
    private void MainWindow_OnSizeChanged(object sender, SizeChangedEventArgs e)
    {
        if(_loaded)
            ScaleEngine.OnWindowSizeChange(sender, e);
    }
    
    private void ShowDebugGridLines()
    {
        if (!ShowGridLines) return;
        
        MasterGrid.ShowGridLines = true;
        TopGrid.ShowGridLines = true;
        NavbarGrid.ShowGridLines = true;
        ContentGrid.ShowGridLines = true;
    }
}