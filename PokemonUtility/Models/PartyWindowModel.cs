using Prism.Mvvm;
using System.Drawing;

namespace PokemonUtility.Models
{
    class PartyWindowModel : BindableBase
    {
        private Point _location = new Point();

        public int X
        {
            get { return _location.X; }
            set { _location.X = value; }
        }

        public int Y
        {
            get { return _location.Y; }
            set { _location.Y = value; }
        }

        private bool _isShowWindow = false;
        public bool IsShowWindow
        {
            get { return _isShowWindow; }
            set { SetProperty(ref _isShowWindow, value); }
        }
    }
}
