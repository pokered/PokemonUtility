using System.Drawing;
using System.Drawing.Imaging;

namespace PokemonUtility.Models.Image
{
    class ScreenCaptureModel
    {
        public static Bitmap ScreenCapture(int x, int y, int width, int height)
        {
            return ScreenCapture(new Rectangle(x, y, width, height));
        }

        public static Bitmap ScreenCapture(Rectangle rectangle)
        {
            Bitmap bmp = new Bitmap(rectangle.Width, rectangle.Height, PixelFormat.Format32bppArgb);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.CopyFromScreen(rectangle.X, rectangle.Y, 0, 0, new Size(rectangle.Width, rectangle.Height), CopyPixelOperation.SourceCopy);
            }
            return bmp;
        }
    }
}
