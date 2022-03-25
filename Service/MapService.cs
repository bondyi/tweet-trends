using TweetTrends.Domain.Model;
using TweetTrends.Persistence.Contexts;

namespace TweetTrends.Service
{
    public class MapService
    {
        public readonly List<State> States;
        public readonly List<Tweet> Tweets;
        public readonly List<Sentiment> Sentiments;

        public MapService()
        {
            States = StatesContext.GetStates();
            Tweets = TweetsContext.GetTweets();
            Sentiments = SentimentsContext.GetSentiments();
        }

        public float GetSentiment(Tweet tweet)
        {
            var sumSentiment = 0.0f;

            foreach (var sentiment in Sentiments)
            {
                if (tweet.Description.Contains(sentiment.Description))
                {
                    sumSentiment += sentiment.Value;
                }
            }

            return sumSentiment;
        }
    }
}