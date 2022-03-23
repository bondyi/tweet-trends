using TweetTrends.Service;

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

            foreach (var state in _mapService.States)
            {
                foreach (var polygon in state.Location)
                {
                    graphics.DrawPolygon(Pens.Black, polygon.ToArray());
                }
            }

            foreach (var tweet in _mapService.Tweets)
            {
                graphics.DrawRectangle(Pens.DarkMagenta, tweet.Location.X, tweet.Location.Y, 1.0f, 1.0f);
            }
        }
    }
}