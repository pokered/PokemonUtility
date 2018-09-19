using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.IO;
using System.Windows.Media.Imaging;

namespace PokemonUtility.Models
{
    class ImageModel
    {
        public BitmapSource createImage(int id)
        {
            String currentDir = Directory.GetCurrentDirectory();
            String imagePath = string.Format("Images/pokemon/{0}/icon.png", id);
            String relativeImagePath = Path.Combine(currentDir, imagePath);
            Mat pokemonImg = Cv2.ImRead(relativeImagePath, ImreadModes.Unchanged);
            Cv2.Resize(pokemonImg, pokemonImg, new Size(80, 80), 0, 0, InterpolationFlags.Lanczos4);


            //// フレーム
            //imagePath = "Images/frame/order_first.png";
            //relativeImagePath = Path.Combine(currentDir, imagePath);
            //Mat frameImg = Cv2.ImRead(relativeImagePath, ImreadModes.Unchanged);
            //Cv2.Resize(frameImg, frameImg, new Size(80, 80), 0, 0, InterpolationFlags.Lanczos4);
            
            //Mat dMat = new Mat();
            //Cv2.AddWeighted(pokemonImg, 0.7, frameImg, 0.3, 2.2, dMat);

            ////前景画像の変形行列
            //Mat mat = new Mat();

            //var center = new Point2f(frameImg.Cols / 2, frameImg.Cols / 2);
            //Mat rMat = Cv2.GetRotationMatrix2D(center, 90, 1);
            //Cv2.WarpAffine(frameImg, pokemonImg, rMat, new Size(80, 80), InterpolationFlags.Linear, BorderTypes.Transparent);

            return BitmapSourceConverter.ToBitmapSource(pokemonImg);
        }

        private string CreateImagePath(int id)
        {
            String currentDir = Directory.GetCurrentDirectory();
            String imagePath = string.Format("Images/pokemon/{0}/icon.png", id);
            String relativeImagePath = Path.Combine(currentDir, imagePath);

            if (File.Exists(relativeImagePath)) return relativeImagePath;

            return Path.Combine(currentDir, "Images/progress/progress3.png");
        }
    }
}
