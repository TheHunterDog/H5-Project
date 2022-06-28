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
            new MapNode(){Id = 1,Letter ="T",LocationCollection = new LocationCollection() {
                new Location(52.501463136223386, 6.078835128702142),
                new Location(52.50102343461829, 6.078606640140385),
                new Location(52.50082937665115, 6.0796329739331565),
                new Location(52.501270198271015, 6.0798559444647315),
            },PaddingVertical = 0.0, PaddingHorizontal = 0.0},
            new MapNode(){Id = 1,Letter ="D", LocationCollection = new LocationCollection()
            {
                new Location(52.50120938554224, 6.080083656137327),
                new Location(52.5009931419597, 6.0812146611997875),
                new Location(52.50060564793444, 6.081032780567894),
                new Location(52.500813240438845, 6.079893259781186),

            },PaddingVertical = 0.0, PaddingHorizontal = 0.0},
            new MapNode(){Id = 1,Letter ="X",LocationCollection = new LocationCollection()
            {
                new Location(52.50005335821603, 6.078794260142196),
                new Location(52.50064020933097, 6.079094609915257),
                new Location(52.50077412325708, 6.078413812904064),
                new Location(52.50017702795095, 6.078027948952866),
                
            }, PaddingVertical = 0.0, PaddingHorizontal = 0.0},
            new MapNode(){Id = 1,Letter ="C", LocationCollection = new LocationCollection()
            {
                new Location(52.49971162007481, 6.081740275665474),
                new Location(52.49999965771733, 6.079779152019304),
                new Location(52.50051101574119, 6.08000886192774),
                new Location(52.499928685044694, 6.0818157111785025),
            }, PaddingVertical = 0.0, PaddingHorizontal = 0.0},
            new MapNode(){Id = 1,Letter ="E",LocationCollection = new LocationCollection()
            {
                new Location(52.50096374283298, 6.081478931484673),
                new Location(52.50090146545368, 6.081802888457474),
                new Location(52.50034443852256, 6.081507345619023),
                new Location(52.500519158615134, 6.081248751368763),
            
            }, PaddingVertical = 0.0, PaddingHorizontal = 0.0},
            new MapNode(){Id = 1,Letter ="F",LocationCollection = new LocationCollection()
            {
                new Location(52.50085747289534, 6.0820591441128755),
                new Location(52.50079485529552, 6.082381429865841),
                new Location(52.500012176700906, 6.081990554196199),
                new Location(52.50018958636694, 6.081716274948171),
            }, PaddingVertical = 0.0, PaddingHorizontal = 0.0},
            
            
            new MapNode(){Id = 1,Letter ="G",LocationCollection = new LocationCollection()
            {
                new Location(52.50019128741016, 6.082750520543131),
                new Location(52.50067132995334, 6.0830042408940965),
                new Location(52.500736033274485, 6.082685384807264),
                new Location(52.500109889895676, 6.082356239353124),
            }, PaddingVertical = 0.0, PaddingHorizontal = 0.0},
            
            
            new MapNode(){Id = 1,Letter ="H",LocationCollection = new LocationCollection()
            {
                new Location(52.50047646117795, 6.083040872914258),
                new Location(52.50058678027224, 6.08322856787796),
                new Location(52.500654744855446, 6.083259313411283),
                new Location(52.500732066884616, 6.083256886745456),
                new Location(52.50079806123194, 6.083223716642305),
                new Location(52.50087489079489, 6.0831314875936044),
                
            }, PaddingVertical = 0.0, PaddingHorizontal = 0.0},
            new MapNode(){Id = 1,Letter ="Z",LocationCollection = new LocationCollection()
            {
                new Location(52.5000752081762, 6.07783579833106),
                new Location(52.49989204067454, 6.07881147282855),
                new Location(52.49942834540147, 6.078593294971163),
                new Location(52.49963227736361, 6.077650447268834),
            }, PaddingVertical = 0.0, PaddingHorizontal = 0.0},
            new MapNode(){Id = 1,Letter ="S",LocationCollection = new LocationCollection()
            {
                new Location(52.49875229365416, 6.079116837370195),
                new Location(52.49894251074827, 6.078261207930719),
                new Location(52.4998733217972, 6.078824571477144),
                new Location(52.49973832819994, 6.079591507406131),
            } , PaddingVertical = 0.0, PaddingHorizontal = 0.0},
            new MapNode(){Id = 1,LocationCollection = new LocationCollection()
            {
                new Location(52.498771578944776, 6.080541266323441),
                new Location(52.499214067604534, 6.080876450584057),
                new Location(52.49943481484116, 6.079644426904879),
                new Location(52.49892910952289, 6.079380058492711),
            },Letter ="A",Longitude =6.080186480709502 , PaddingVertical = 0.0, PaddingHorizontal = 0.0},
            new MapNode(){Id = 1,Letter ="B",LocationCollection = new LocationCollection()
            {
                new Location(52.49971162007481, 6.081740275665474),
                new Location(52.49999965771733, 6.079779152019304),
                
                new Location(52.49952621674873, 6.079533980048299),
                new Location(52.49923242550393, 6.081489069112934),
            }, PaddingVertical = 0.0, PaddingHorizontal = 0.0},
              
              
        };
        
        public MapWindow()
        {
            InitializeComponent();
            placePlot();
            placeNodes();
            
        }


        public void placeNodes()
        {
            foreach (var node in Nodes)
            {
                Pushpin pin = new Pushpin();
                double minLongitude = node.LocationCollection[0].Longitude, maxLongitude = node.LocationCollection[0].Longitude;
                double minLatitude = node.LocationCollection[0].Latitude, maxLatitude = node.LocationCollection[0].Latitude;
                double longSum = 0;
                double latSum = 0;
                foreach (var location in node.LocationCollection)
                {
                    longSum += location.Longitude;
                    latSum += location.Latitude;
                }
                pin.Location = new Location((latSum) / node.LocationCollection.Count, (longSum)/node.LocationCollection.Count);
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
                // (paddingH,paddingV) = calculatePadding(node, new Location(52.500774475460105, 6.078416565693611));
                MapPolygon polygon = new MapPolygon();
                polygon.Fill = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Yellow);
                polygon.Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Pink);
                polygon.StrokeThickness = 5;
                polygon.Opacity = 0.7;
                polygon.Locations =  node.LocationCollection;

                Map.Children.Add(polygon);

            }
        }
    }
}
