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

        private float GetAverageSentiment(Tweet tweet)
        {
            var sumSentiment = 0.0f;
            var count = 0;

            foreach (var sentiment in Sentiments)
            {
                if (tweet.Description.Contains(sentiment.Description))
                {
                    sumSentiment += sentiment.Value;
                    ++count;
                }
            }

            return sumSentiment / count;
        }
    }
}