using TweetTrends.Domain.Model;
using System.Drawing;

namespace TweetTrends.Persistence.Contexts
{
    public static class TweetsContext
    {
        public static List<Tweet> GetTweets()
        {
            var tweets = new List<Tweet>();

            var data = File.ReadAllText(FilePaths.Tweets);
            var array = data.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var tweet in array)
            {
                var formattedTweet = tweet.Split(new char[] { '\t', '_' }, StringSplitOptions.RemoveEmptyEntries);

                var location = formattedTweet[0].Split(new char[] { '[', ']', ',' }, StringSplitOptions.RemoveEmptyEntries);

                var x = Convert.ToDouble(location[1].Replace('.', ',')) * 33.0f + 4125.0f;
                var y = Convert.ToDouble(location[0].Replace('.', ',')) * -33.0f + 1750.0f;

                tweets.Add(new Tweet(
                    new PointF((float) x, (float) y),
                    DateTime.Parse(formattedTweet[1]), 
                    formattedTweet[2]));

            }

            return tweets;
        }
    }
}