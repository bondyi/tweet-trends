using System.Drawing;

namespace TweetTrends.Domain.Model;

public class Tweet
{
    public readonly PointF Location;
    public readonly DateTime PublicationTime;
    public readonly string Description;


    public Tweet(PointF location, DateTime publicationTime, string description)
    {
        Location = location;
        PublicationTime = publicationTime;
        Description = description;
    }

    public override string ToString()
    {
        return $"[{Location.X}, {Location.Y}] {PublicationTime} {Description}";
    }
}