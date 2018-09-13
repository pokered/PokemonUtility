using Prism.Mvvm;
using System.Windows.Media.Imaging;

namespace PokemonUtility.Models
{
    class PokemonImageModel : BindableBase
    {
        private int _pokemonID = -1;
        public int PokemonID
        {
            get { return _pokemonID; }
            set { SetProperty(ref _pokemonID, value); }
        }

        private BitmapImage bmp;
        public BitmapImage Bmp
        {
            get { return bmp; }
            set { SetProperty(ref bmp, value); }
        }
    }
}
