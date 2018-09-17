using Prism.Mvvm;
using System;
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

        private BitmapImage _image;
        public BitmapImage Image
        {
            get { return _image; }
            set { SetProperty(ref _image, value); }
        }

        private void ChangeImage()
        {
            // UIスレッドで行う例
            var source = new BitmapImage();
            BitmapImage img = new BitmapImage();
            img.BeginInit();
            img.UriSource = new Uri("C:\\Users\\pokered\\Source\\Repos\\PokemonUtility\\PokemonUtility\\Images\\pokemon\\0\\icon.png");
            img.EndInit();

            Image = img;
        }

        public PokemonImageModel()
        {
            Image = new BitmapImage();
            ID = 0;
        }
    }
}
