using PokemonUtility.Models.Abstract;

namespace PokemonUtility.Models.Capture
{
    class CaptureWindowModel : WindowModel
    {
        #region Singleton

        static CaptureWindowModel Instance;
        public static CaptureWindowModel GetInstance()
        {
            if (Instance == null)
                Instance = new CaptureWindowModel();
            return Instance;
        }

        #endregion

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
