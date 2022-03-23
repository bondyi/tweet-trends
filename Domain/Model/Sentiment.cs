namespace TweetTrends.Domain.Model
{
    public struct Sentiment
    {
        public readonly string Description;
        public readonly float Value;

        public Sentiment(string description, double value)
        {
            Description = description;
            Value = (float) value;
        }
    }
}
