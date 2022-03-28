using System.Drawing;
using System.Text.Json;
using TweetTrends.Domain.Model;

namespace TweetTrends.Persistence.Contexts
{
    public static class StatesContext
    {

        public static List<State> GetStates()
        {
            var cin = new StreamReader(FilePaths.States);
            var document = JsonDocument.Parse(cin.ReadToEnd());

            var points = new List<PointF>();
            var polygons = new List<List<PointF>>();

            var states = new List<State>();

            foreach (var state in document.RootElement.EnumerateObject())
            {
                foreach (var shape in state.Value.EnumerateArray())
                {
                    var polygon = shape;
                    if (polygon[0][0].ValueKind != JsonValueKind.Number) polygon = polygon[0];

                    foreach (var point in polygon.EnumerateArray())
                    {
                        points.Add(new PointF(
                            (float)point[0].GetDouble() * 15.0f + 2800.0f,
                            (float)point[1].GetDouble() * -15.0f + 1200.0f));
                    }

                    polygons.Add(points);
                    points = new List<PointF>();
                }

                var postalCode = state.Name;
                states.Add(new State(postalCode, polygons));

                polygons = new List<List<PointF>>();
            }

            cin.Close();

            return states;
        }
    }
}