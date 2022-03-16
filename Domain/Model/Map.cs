using System.Text;

namespace TweetTrends.Domain.Model;

public class Map
{
    public readonly List<State> States;

    public Map(List<State> states) => States = states;

    public override string ToString()
    {
        var stringBuilder = new StringBuilder();
        foreach (var state in States)
        {
            stringBuilder.Append(state + "\n");
        }

        return stringBuilder.ToString();
    }
}