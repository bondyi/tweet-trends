using TweetTrends.Service;
using System.Drawing.Drawing2D;
using TweetTrends.Domain.Model;
using TweetTrends.Persistence.Contexts;

namespace TweetTrends.DesktopApp
{
    public partial class Form1 : Form
    {
        private MapService _mapService;

        public Form1()
        {
            InitializeComponent();

            _mapService = new MapService();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            var graphics = CreateGraphics();

            graphics.DrawString(FilePaths.Tweets, new Font("Arial", 10f), new SolidBrush(Color.Black), 0, 0);

            Rectangle rectangle = new Rectangle(10, 800, 20, 20);
            graphics.FillRectangle(new SolidBrush(Color.DarkBlue), rectangle);
            graphics.DrawRectangle(new Pen(Color.Black), rectangle);
            graphics.DrawString(@"Unknown", new Font("Arial", 10f), new SolidBrush(Color.Black), 35, 802);

            rectangle = new Rectangle(10, 820, 20, 20);
            graphics.FillRectangle(new SolidBrush(Color.FromArgb(204, 0, 0)), rectangle);
            graphics.DrawRectangle(new Pen(Color.Black), rectangle);
            graphics.DrawString(@"<= -0.1", new Font("Arial", 10f), new SolidBrush(Color.Black), 35, 822);

            rectangle = new Rectangle(10, 840, 20, 20);
            graphics.FillRectangle(new SolidBrush(Color.Orange), rectangle);
            graphics.DrawRectangle(new Pen(Color.Black), rectangle);
            graphics.DrawString(@"> -0.1 and <= 0", new Font("Arial", 10f), new SolidBrush(Color.Black), 35, 842);

            rectangle = new Rectangle(10, 860, 20, 20);
            graphics.FillRectangle(new SolidBrush(Color.Yellow), rectangle);
            graphics.DrawRectangle(new Pen(Color.Black), rectangle);
            graphics.DrawString(@"> 0 and <= 0.1", new Font("Arial", 10f), new SolidBrush(Color.Black), 35, 862);

            rectangle = new Rectangle(10, 880, 20, 20);
            graphics.FillRectangle(new SolidBrush(Color.Green), rectangle);
            graphics.DrawRectangle(new Pen(Color.Black), rectangle);
            graphics.DrawString(@"> 0.1 and <= 0.2", new Font("Arial", 10f), new SolidBrush(Color.Black), 35, 882);

            rectangle = new Rectangle(10, 900, 20, 20);
            graphics.FillRectangle(new SolidBrush(Color.Aqua), rectangle);
            graphics.DrawRectangle(new Pen(Color.Black), rectangle);
            graphics.DrawString(@"> 0.2", new Font("Arial", 10f), new SolidBrush(Color.Black), 35, 902);

            var graphicsPaths = new List<GraphicsPath>();
            var tweets = new List<Tweet>();

            foreach (var state in _mapService.States)
            {
                foreach (var polygon in state.Location)
                {
                    byte[] types = new byte[polygon.Count];
                    for (int i = 0; i < polygon.Count; ++i) types[i] = 1;

                    graphicsPaths.Add(new GraphicsPath(polygon.ToArray(), types));
                }
            }

            foreach (var graphicsPath in graphicsPaths)
            {
                var averageSentiment = 0.0f;
                var count = 0;

                foreach (var tweet in _mapService.Tweets)
                {
                    if (graphicsPath.IsVisible(tweet.Location))
                    {
                        tweets.Add(tweet);

                        averageSentiment += _mapService.GetSentiment(tweet);
                        ++count;
                    }
                }

                averageSentiment = count == 0 ? float.NaN : (averageSentiment / count);

                if (float.IsNaN(averageSentiment)) graphics.FillPath(new SolidBrush(Color.DarkBlue), graphicsPath);
                else if (averageSentiment <= -0.1f) graphics.FillPath(new SolidBrush(Color.FromArgb(204, 0, 0)), graphicsPath);
                else if (averageSentiment > -0.1f && averageSentiment <= 0.0f) graphics.FillPath(new SolidBrush(Color.Orange), graphicsPath);
                else if (averageSentiment > 0.0f && averageSentiment <= 0.1f) graphics.FillPath(new SolidBrush(Color.Yellow), graphicsPath);
                else if (averageSentiment > 0.1f && averageSentiment <= 0.2f) graphics.FillPath(new SolidBrush(Color.Green), graphicsPath);
                else if (averageSentiment > 0.2f) graphics.FillPath(new SolidBrush(Color.Aqua), graphicsPath);

                graphics.DrawPath(Pens.Black, graphicsPath);
            }


            foreach (var tweet in tweets)
            {
                graphics.FillEllipse(Brushes.Blue, tweet.Location.X, tweet.Location.Y, 5.0f, 5.0f);
                graphics.DrawEllipse(Pens.White, tweet.Location.X, tweet.Location.Y, 5.0f, 5.0f);
            }
        }
    }
}