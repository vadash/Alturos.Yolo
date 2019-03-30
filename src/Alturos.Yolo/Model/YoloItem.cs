using System.Drawing;

namespace Alturos.Yolo.Model
{
    public class YoloItem
    {
        public string Type { get; set; }
        public double Confidence { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }

        public Point Center()
        {
            return new Point((int) (this.X + this.Width / 2), (int) (this.Y + this.Height / 2));
        }
    }
}
