using Prism.Mvvm;
using System;
using System.IO;
using System.Windows.Media.Imaging;

namespace PokemonUtility.Models
{
    class PartyImageModel : BindableBase
    {
        public PokemonImageModel Pokemon1 { get; }
        public PokemonImageModel Pokemon2 { get; }
        public PokemonImageModel Pokemon3 { get; }
        public PokemonImageModel Pokemon4 { get; }
        public PokemonImageModel Pokemon5 { get; }
        public PokemonImageModel Pokemon6 { get; }

        // 選出順
        private BitmapImage _frame = new BitmapImage();
        public BitmapImage Frame
        {
            get { return _frame; }
            set { SetProperty(ref _frame, value); }
        }

        public PartyImageModel()
        {
            Pokemon1 = new PokemonImageModel();
            Pokemon2 = new PokemonImageModel();
            Pokemon3 = new PokemonImageModel();
            Pokemon4 = new PokemonImageModel();
            Pokemon5 = new PokemonImageModel();
            Pokemon6 = new PokemonImageModel();

            string currentDir = Directory.GetCurrentDirectory();

            BitmapImage tmpBmp = new BitmapImage();
            tmpBmp.BeginInit();
            tmpBmp.UriSource = new Uri(Path.Combine(currentDir, "Images/frame/order_first.png"));
            tmpBmp.DecodePixelWidth = 200;
            tmpBmp.DecodePixelHeight = 200;
            tmpBmp.EndInit();

            Frame = tmpBmp;
        }
    }
}
