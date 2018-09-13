using Prism.Mvvm;
using System.Drawing;

namespace PokemonUtility.Models
{
    class MyPartyWindowModel : BindableBase
    {
        #region Singleton

        static MyPartyWindowModel Instance;
        public static MyPartyWindowModel GetInstance()
        {
            if (Instance == null)
                Instance = new MyPartyWindowModel();
            return Instance;
        }

        #endregion

        private Point _location;

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

        private MyPartyWindowModel()
        {
            _location = new Point();
        }
    }
}
