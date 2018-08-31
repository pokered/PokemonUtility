using Prism.Interactivity.InteractionRequest;
using System.Drawing;

namespace PokemonUtility.Models
{
    public class RectangleNotification : Notification
    {
        private Rectangle _rectangle = new Rectangle();

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
    }
}
