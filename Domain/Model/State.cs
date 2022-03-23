using System.Drawing;
using System.Text;

namespace TweetTrends.Domain.Model
{
    public class State
    {
        public readonly string PostalCode;
        public readonly List<List<PointF>> Location;

        public State(string postalCode, List<List<PointF>> location)
        {
            PostalCode = postalCode;
            Location = location;
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            foreach (var state in Location)
            {
                stringBuilder.Append(PostalCode + ":\n");

                foreach (var point in state)
                {
                    stringBuilder.Append($"[{point.X}, {point.Y}]\n");
                }

                stringBuilder.AppendLine();
            }

            return stringBuilder.ToString();
        }
    }
}