using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using Database.Model;
using Microsoft.Maps.MapControl.WPF;

namespace WPF.Util;

public class MapAssist
{
    #region SeedData

    private static readonly List<MapNode> LocalNodes = new List<MapNode>
        {
            new MapNode(){Letter ="T",LocationCollection = new LocationCollection() {
                new Location(52.501463136223386, 6.078835128702142),
                new Location(52.50102343461829, 6.078606640140385),
                new Location(52.50082937665115, 6.0796329739331565),
                new Location(52.501270198271015, 6.0798559444647315),
            },},
            new MapNode(){Letter ="D", LocationCollection = new LocationCollection()
            {
                new Location(52.50120938554224, 6.080083656137327),
                new Location(52.5009931419597, 6.0812146611997875),
                new Location(52.50060564793444, 6.081032780567894),
                new Location(52.500813240438845, 6.079893259781186),
    
            },},
            new MapNode(){Letter ="X",LocationCollection = new LocationCollection()
            {
                new Location(52.50005335821603, 6.078794260142196),
                new Location(52.50064020933097, 6.079094609915257),
                new Location(52.50077412325708, 6.078413812904064),
                new Location(52.50017702795095, 6.078027948952866),
                
            }, },
            new MapNode(){Letter ="C", LocationCollection = new LocationCollection()
            {
                new Location(52.49971162007481, 6.081740275665474),
                new Location(52.49999965771733, 6.079779152019304),
                new Location(52.50051101574119, 6.08000886192774),
                new Location(52.499928685044694, 6.0818157111785025),
            },},
            new MapNode(){Letter ="E",LocationCollection = new LocationCollection()
            {
                new Location(52.50096374283298, 6.081478931484673),
                new Location(52.50090146545368, 6.081802888457474),
                new Location(52.50034443852256, 6.081507345619023),
                new Location(52.500519158615134, 6.081248751368763),
            
            },},
            new MapNode(){Letter ="F",LocationCollection = new LocationCollection()
            {
                new Location(52.50085747289534, 6.0820591441128755),
                new Location(52.50079485529552, 6.082381429865841),
                new Location(52.500012176700906, 6.081990554196199),
                new Location(52.50018958636694, 6.081716274948171),
            }, },
            
            
            new MapNode(){Letter ="G",LocationCollection = new LocationCollection()
            {
                new Location(52.50019128741016, 6.082750520543131),
                new Location(52.50067132995334, 6.0830042408940965),
                new Location(52.500736033274485, 6.082685384807264),
                new Location(52.500109889895676, 6.082356239353124),
            }, },
            
            
            new MapNode(){Letter ="H",LocationCollection = new LocationCollection()
            {
                new Location(52.50047646117795, 6.083040872914258),
                new Location(52.50058678027224, 6.08322856787796),
                new Location(52.500654744855446, 6.083259313411283),
                new Location(52.500732066884616, 6.083256886745456),
                new Location(52.50079806123194, 6.083223716642305),
                new Location(52.50087489079489, 6.0831314875936044),
                
            }, },
            new MapNode(){Letter ="Z",LocationCollection = new LocationCollection()
            {
                new Location(52.5000752081762, 6.07783579833106),
                new Location(52.49989204067454, 6.07881147282855),
                new Location(52.49942834540147, 6.078593294971163),
                new Location(52.49963227736361, 6.077650447268834),
            }, },
            new MapNode(){Letter ="S",LocationCollection = new LocationCollection()
            {
                new Location(52.49875229365416, 6.079116837370195),
                new Location(52.49894251074827, 6.078261207930719),
                new Location(52.4998733217972, 6.078824571477144),
                new Location(52.49973832819994, 6.079591507406131),
            } , },
            new MapNode(){LocationCollection = new LocationCollection()
            {
                new Location(52.498771578944776, 6.080541266323441),
                new Location(52.499214067604534, 6.080876450584057),
                new Location(52.49943481484116, 6.079644426904879),
                new Location(52.49892910952289, 6.079380058492711),
            },Letter ="A", },
            new MapNode(){Letter ="B",LocationCollection = new LocationCollection()
            {
                new Location(52.49971162007481, 6.081740275665474),
                new Location(52.49999965771733, 6.079779152019304),
                new Location(52.49952621674873, 6.079533980048299),
                new Location(52.49923242550393, 6.081489069112934),
            }, },
              
              
        };
    #endregion


    public MapAssist(bool seeding)
    {
        if (seeding)
        {
            SeedDatabase();
        }
    }

    public MapAssist()
    {
    }

    private static void SeedDatabase()
    {
        using var d = new DatabaseContext();
        if (d.MapNodes.Any()) return;
        foreach (var mapnode in LocalNodes)
        {

            d.MapNodes.Add(mapnode);
        }

        d.SaveChanges();
    }

    private static MapLayer GetLayer(Location center, string letter)
    {
        var layer = new MapLayer();
        MapLayer.SetPosition(layer,center);
        layer.Children.Add(new TextBlock()
        {
            Text = letter
        });
        return layer;
    }

    public void PlaceNodes(Map map)
    {
        using var ctx = new DatabaseContext();
        foreach (var node in ctx.MapNodes)
        {
            double longSum = 0;
            double latSum = 0;
            foreach (var location in node.LocationCollection)
            {
                longSum += location.Longitude;
                latSum += location.Latitude;
            }

            map.Children.Add(GetLayer(
                new Location((latSum) / node.LocationCollection.Count, (longSum) / node.LocationCollection.Count),
                node.Letter));
        }
    }

    public static void PlacePlot(Map map)
    {
        using var ctx = new DatabaseContext();
        foreach (var node in ctx.MapNodes)
        {
            var polygon = new MapPolygon
            {
                Fill = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.White),
                Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Yellow),
                StrokeThickness = 5,
                Opacity = 0.7,
                Locations = node.LocationCollection
            };

            map.Children.Add(polygon);

        }
    }
}