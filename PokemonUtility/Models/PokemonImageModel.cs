using Prism.Mvvm;
using System.Windows.Media.Imaging;

namespace PokemonUtility.Models
{
    class PokemonImageModel : BindableBase
    {
        private int _id = -1;
        public int ID
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

        private BitmapImage _image;
        public BitmapImage Image
        {
            get { return _image; }
            set { SetProperty(ref _image, value); }
        }
    }
}
