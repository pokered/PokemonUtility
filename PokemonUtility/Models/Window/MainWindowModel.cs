using Prism.Mvvm;
using System;
using System.IO;
using System.Windows.Media.Imaging;

namespace PokemonUtility.Models.Window
{
    class MainWindowModel : BindableBase
    {
        #region Singleton

        static MainWindowModel Instance;
        public static MainWindowModel GetInstance()
        {
            if (Instance == null)
                Instance = new MainWindowModel();
            return Instance;
        }

        #endregion

        private BitmapImage _captureImage;
        public BitmapImage CaptureImage
        {
            get { return _captureImage; }
            set { SetProperty(ref _captureImage, value); }
        }

        public MainWindowModel()
        {
            ChangeImage(0);
        }

        public void ChangeImage(int id)
        {
            string currentDir = Directory.GetCurrentDirectory();
            string pokemonImagePath = string.Format("Images/pokemon/{0}/icon.png", id);
            string relativePokemonImagePath = Path.Combine(currentDir, pokemonImagePath);

            // イメージ取得
            BitmapImage pokemonImage = new BitmapImage();
            pokemonImage.BeginInit();
            pokemonImage.UriSource = new Uri(relativePokemonImagePath);
            pokemonImage.EndInit();

            CaptureImage = pokemonImage;
        }
    }
}
