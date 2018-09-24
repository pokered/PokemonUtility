using Prism.Mvvm;

namespace PokemonUtility.Models
{
    class WindowModel : BindableBase
    {
        private int _x = 0;
        public int X
        {
            get { return _x; }
            set { SetProperty(ref _x, value); }
        }

        private int _y = 0;
        public int Y
        {
            get { return _y; }
            set { SetProperty(ref _y, value); }
        }

        private int _width = 1;
        public int Width
        {
            get { return _width; }
            set { SetProperty(ref _width, value); }
        }

        private int _height = 1;
        public int Height
        {
            get { return _height; }
            set { SetProperty(ref _height, value); }
        }
    }
}
