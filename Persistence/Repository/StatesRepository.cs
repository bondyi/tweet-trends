using System.Drawing;
using System.Text.Json;
using TweetTrends.Domain.Model;

namespace TweetTrends.Persistence.Repository;

public static class StatesRepository
{
    public const string FilePath = "Resources/states.json";

    public static Map GetMap()
    {
        var cin = new StreamReader(FilePath);
        var document = JsonDocument.Parse(cin.ReadToEnd());
        
        var points = new List<PointF>();
        var polygons = new List<List<PointF>>();
        
        var states = new List<State>();

        foreach (var state in document.RootElement.EnumerateObject())
        {
            foreach (var shape in state.Value.EnumerateArray())
            {
                var polygon = shape;
                if(polygon[0][0].ValueKind != JsonValueKind.Number) polygon = polygon[0]; 
                
                foreach (var point in polygon.EnumerateArray())
                {
                    points.Add(new PointF(
                        (float)point[0].GetDouble() * 16.0f + 2900.0f,
                        (float)point[1].GetDouble() * -16.0f + 1200.0f));
                }
                
                polygons.Add(points);
                points = new List<PointF>();
            }

            var postalCode = state.Name;
            states.Add(new State(postalCode, polygons));
            
            polygons = new List<List<PointF>>();
        }
        
        cin.Close();

        return new Map(states);
    }
}