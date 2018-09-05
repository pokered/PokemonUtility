using System.Drawing;

namespace PokemonUtility.Models
{
    public class WindowRectangle
    {
        private Rectangle _rectangle;

        public int X
        {
            get { return _rectangle.X; }
            set { _rectangle.X = value; }
        }

        public int Y
        {
            get { return _rectangle.Y; }
            set { _rectangle.Y = value; }
        }

        public int Width
        {
            get { return _rectangle.Width; }
            set { _rectangle.Width = value; }
        }

        public int Height
        {
            get { return _rectangle.Height; }
            set { _rectangle.Height = value; }
        }

        public WindowRectangle()
        {
            _rectangle = new Rectangle();
        }
    }
}
