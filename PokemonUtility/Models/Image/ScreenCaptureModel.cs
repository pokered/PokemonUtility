using System.Drawing;
using System.Drawing.Imaging;

namespace PokemonUtility.Models.Image
{
    class ScreenCaptureModel
    {
        public static Bitmap ScreenCapture(int x, int y, int width, int height)
        {
            Bitmap bmp = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.CopyFromScreen(x, y, 0, 0, new Size(width, height), CopyPixelOperation.SourceCopy);
            }
            return bmp;
        }
    }
}
