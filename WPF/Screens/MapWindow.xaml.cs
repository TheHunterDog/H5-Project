#region

using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using Database.Model;
using Microsoft.Maps.MapControl.WPF;
using WPF.Util;

#endregion

namespace WPF.Screens
{
    /**
     * <summary>Interaction logic for MapWindow.xaml</summary>
     */
    // <wpf:Pushpin Location="52.50107401581845, 6.079146517870714" Content="T"/>
    //     <wpf:Pushpin Location="52.500923791962734, 6.080712958728256" Content="D"/>
    //     <wpf:Pushpin Location="52.50035230568534, 6.078647631938577" Content="X"/>
    //     <wpf:Pushpin Location="52.50001920899266, 6.080696842326544" Content="C"/>
    //     <wpf:Pushpin Location="52.50069192111236, 6.081522991392552" Content="E"/>
    //     <wpf:Pushpin Location="52.50046005144939, 6.082016512539299" Content="F"/>
    
    
    //     <wpf:Pushpin Location="52.5005220814926, 6.082762179955078" Content="G"/>
    //     <wpf:Pushpin Location="52.500682120525745, 6.083167147146193" Content="H"/>
    //     <wpf:Pushpin Location="52.499773620755064, 6.078321891079488" Content="Z"/>
    //     <wpf:Pushpin Location="52.49916731581769, 6.0789601186802456" Content="S"/>
    //     <wpf:Pushpin Location="52.499071882476706, 6.080186480709502" Content="A"/>
    //     <wpf:Pushpin Location="52.49956131951883, 6.080314542718152" Content="B"/>
    public partial class MapWindow : Window
    {
        static List<MapNode> Nodes = new List<MapNode>
        {
            // new MapNode(){Id = 1,Latitude = 52.50107401581845,Letter ="T",Longitude =6.079146517870714, PaddingVertical = 0.0, PaddingHorizontal = 0.0},
            // new MapNode(){Id = 1,Latitude = 52.500923791962734,Letter ="D",Longitude =6.080712958728256, PaddingVertical = 0.0, PaddingHorizontal = 0.0},
            new MapNode(){Id = 1,Latitude = 52.5003966701037,Letter ="X",Longitude =6.078595993618545 , PaddingVertical = 0.0, PaddingHorizontal = 0.0},
            // new MapNode(){Id = 1,Latitude = 52.50001920899266,Letter ="C",Longitude =6.080696842326544 , PaddingVertical = 0.0, PaddingHorizontal = 0.0},
            // new MapNode(){Id = 1,Latitude = 52.50069192111236,Letter ="E",Longitude =6.081522991392552 , PaddingVertical = 0.0, PaddingHorizontal = 0.0},
            // new MapNode(){Id = 1,Latitude = 52.50046005144939,Letter ="F",Longitude =6.082016512539299 , PaddingVertical = 0.0, PaddingHorizontal = 0.0},
            // new MapNode(){Id = 1,Latitude = 52.5005220814926,Letter ="G",Longitude =6.082762179955078 , PaddingVertical = 0.0, PaddingHorizontal = 0.0},
            // new MapNode(){Id = 1,Latitude = 52.5005220814926,Letter ="H",Longitude =6.083167147146193 , PaddingVertical = 0.0, PaddingHorizontal = 0.0},
            // new MapNode(){Id = 1,Latitude = 52.499773620755064,Letter ="Z",Longitude =6.078321891079488 , PaddingVertical = 0.0, PaddingHorizontal = 0.0},
            // new MapNode(){Id = 1,Latitude = 52.49916731581769,Letter ="S",Longitude =6.0789601186802456 , PaddingVertical = 0.0, PaddingHorizontal = 0.0},
            // new MapNode(){Id = 1,Latitude = 52.499071882476706,Letter ="A",Longitude =6.080186480709502 , PaddingVertical = 0.0, PaddingHorizontal = 0.0},
            // new MapNode(){Id = 1,Latitude = 52.49956131951883,Letter ="B",Longitude =6.080314542718152 , PaddingVertical = 0.0, PaddingHorizontal = 0.0},
              
              
        };
        public MapWindow()
        {
            InitializeComponent();
            placePlot();
            placePlotX();
            placeNodes();
            
        }

        public (double , double ) calculatePadding(MapNode node, Location TopLeftcorner)
        {
            double paddingH = TopLeftcorner.Latitude - node.Latitude;
            double paddingV = TopLeftcorner.Longitude - node.Longitude;
            return (Math.Abs(paddingH), Math.Abs(paddingV));
        }

        public void placeNodes()
        {
            foreach (var node in Nodes)
            {
                Pushpin pin = new Pushpin();
                pin.Location = new Location(node.Latitude, node.Longitude);
                pin.Content = node.Letter;
                Map.Children.Add(pin);
            }

        }

        public void placePlot()
        {
            double paddingH;
            double paddingV ;
            
            foreach (var node in Nodes)
            {
                (paddingH,paddingV) = calculatePadding(node, new Location(52.500774475460105, 6.078416565693611));
                MapPolygon polygon = new MapPolygon();
                polygon.Fill = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Yellow);
                polygon.Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Pink);
                polygon.StrokeThickness = 5;
                polygon.Opacity = 0.7;
                polygon.Locations = new LocationCollection()
                {
                    // new Location(node.Latitude - paddingH, node.Longitude + paddingV),
                    new Location(node.Latitude + paddingH, node.Longitude + paddingV),
                    new Location(node.Latitude + paddingH, node.Longitude - paddingV),
                    // new Location(node.Latitude - paddingH, node.Longitude - paddingV)
                };

                Map.Children.Add(polygon);
                MapPolygon p = new MapPolygon();
                p.Fill = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Purple);
                p.Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Purple);
                p.StrokeThickness = 5;
                p.Opacity = 0.7;
                p.Locations = new LocationCollection()
                {
                    new Location(node.Latitude - paddingH, node.Longitude + paddingV),
                    new Location(node.Latitude + paddingH, node.Longitude + paddingV),
                    new Location(node.Latitude + paddingH, node.Longitude - paddingV),
                    new Location(node.Latitude - paddingH, node.Longitude - paddingV)
                };

                Map.Children.Add(p);
                
            }
        }
        public void placePlotX()
        {
            const double padding = 0.000100;
            foreach (var node in Nodes)
            {
                MapPolygon polygon = new MapPolygon();
                polygon.Fill = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Blue);
                polygon.Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Green);
                polygon.StrokeThickness = 5;
                polygon.Opacity = 0.7;
                polygon.Locations = new LocationCollection()
                {
                    new Location(52.50077272362442, 6.078415296688426),
                    new Location(52.50063841293961, 6.079099092421204),
                    new Location(52.50005836266992, 6.078797662254046),
                    new Location(52.50018279195252, 6.078116042826805),
                    
                };

                Map.Children.Add(polygon);
            }
        }
    }
}
