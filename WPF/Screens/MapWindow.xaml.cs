#region

using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Xml.Linq;
using Database.Model;
using Microsoft.Maps.MapControl.WPF;
using WPF.Util;

#endregion

namespace WPF.Screens
{
    /**
     * <summary>Interaction logic for MapWindow.xaml</summary>
     */
    public partial class MapWindow : Window
    {
        public MapWindow()
        {
            InitializeComponent();
            MapAssist mapAssist = new MapAssist();
            // mapAssist.storeInDatabase();
            MapAssist.PlacePlot(Map);
            mapAssist.PlaceNodes(Map);
        }
    }
}
