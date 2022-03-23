using TweetTrends.Domain.Model;

namespace TweetTrends.Persistence.Contexts
{
    public static class SentimentsContext
    {
        public static List<Sentiment> GetSentiments()
        {
            var sentiments = new List<Sentiment>();

            var data = File.ReadAllText(FilePaths.Sentiments);
            var array = data.Split(new char[] {',', '\r', '\n'}, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < array.Length; i += 2)
            {
                sentiments.Add(new Sentiment(array[i], Convert.ToDouble(array[i + 1].Replace('.', ','))));
            }

            return sentiments;
        }
    }
}