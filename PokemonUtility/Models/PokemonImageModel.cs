using OpenCvSharp;
using OpenCvSharp.Extensions;
using Prism.Mvvm;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PokemonUtility.Models
{
    class PokemonImageModel : BindableBase
    {
        private int _id = -1;
        public int ID
        {
            get { return _id; }
            set
            {
                SetProperty(ref _id, value);
                ChangeImage();
            }
        }

        private BitmapImage _image = new BitmapImage();
        public BitmapImage Image
        {
            get { return _image; }
            set { SetProperty(ref _image, value); }
        }

        private BitmapImage _frame = new BitmapImage();
        public BitmapImage Frame
        {
            get { return _frame; }
            set { SetProperty(ref _frame, value); }
        }

        private void ChangeImage()
        {
            Image.UriSource = new Uri(CreateImagePath());
            Image.DecodePixelWidth = 200;
            Image.DecodePixelHeight = 200;
        }

        //private void test()
        //{
        //    // オリジナルポケモン画像
        //    String currentDir = Directory.GetCurrentDirectory();
        //    String imagePath = string.Format("Images/pokemon/{0}/icon.png", 0);
        //    String relativeImagePath = Path.Combine(currentDir, imagePath);
        //    var PokemonImg = new Mat(relativeImagePath);

        //    var ResizedPokemonImg = new Mat();
        //    Cv2.Resize(PokemonImg, ResizedPokemonImg, new OpenCvSharp.Size(80, 80), 0, 0, InterpolationFlags.Lanczos4);

        //    // フレーム
        //    imagePath = "Images/frame/order_first.png";
        //    relativeImagePath = Path.Combine(currentDir, imagePath);
        //    var frameImg = new Mat(relativeImagePath);

        //    var ResizedFrameImg = new Mat();
        //    Cv2.Resize(frameImg, ResizedFrameImg, new OpenCvSharp.Size(80, 80), 0, 0, InterpolationFlags.Lanczos4);

        //    var pastedMat = new Mat();
        //    Cv2.Add(ResizedPokemonImg, ResizedFrameImg, pastedMat);

        //    BitmapSource bitmap = BitmapSourceConverter.ToBitmapSource(pastedMat);
        //    Image = bitmap;

        //    // 破棄
        //    ResizedPokemonImg.Dispose();
        //    ResizedFrameImg.Dispose();
        //    pastedMat.Dispose();
        //}


        private string CreateImagePath()
        {
            String currentDir = Directory.GetCurrentDirectory();
            String imagePath = string.Format("Images/pokemon/{0}/icon.png", ID);
            String relativeImagePath = Path.Combine(currentDir, imagePath);

            if (File.Exists(relativeImagePath)) return relativeImagePath;

            return Path.Combine(currentDir, "Images/progress/progress3.png");
        }

        public PokemonImageModel()
        {
            // イメージ初期化
            Image.BeginInit();
            Image.UriSource = new Uri(CreateImagePath());
            Image.DecodePixelWidth = 200;
            Image.DecodePixelHeight = 200;
            Image.EndInit();
        }
    }
}
