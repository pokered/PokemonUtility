using Prism.Mvvm;
using System;
using System.IO;
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
            BitmapImage tmpBmp = new BitmapImage();
            tmpBmp.BeginInit();
            tmpBmp.UriSource = new Uri(CreateImagePath());
            tmpBmp.DecodePixelWidth = 200;
            tmpBmp.DecodePixelHeight = 200;
            tmpBmp.EndInit();

            Image = tmpBmp;
        }

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
            ID = -1;
        }
    }
}
