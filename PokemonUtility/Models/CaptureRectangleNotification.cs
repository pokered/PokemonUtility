using Prism.Interactivity.InteractionRequest;
using System.Drawing;

namespace PokemonUtility.Models
{
    public class CaptureRectangleNotification : Notification
    {
        public Rectangle CaptureRectangle { get; set; }
    }
}
