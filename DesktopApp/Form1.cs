using TweetTrends.Persistence.Repository;

namespace TweetTrends.DesktopApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            var map = StatesRepository.GetMap();

            var graphics = CreateGraphics();

            foreach(var state in map.States)
            {
                foreach(var polygon in state.Location)
                {
                    graphics.DrawPolygon(Pens.Black, polygon.ToArray());
                }

                graphics.DrawString(state.PostalCode, DefaultFont, Brushes.Red, state.Center);
            }
        }
    }
}