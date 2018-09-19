using PokemonUtility.Models;
using Prism.Commands;
using Prism.Mvvm;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.IO;
using System.Windows.Media.Imaging;

namespace PokemonUtility.ViewModels
{
    public class MyPartyWindowViewModel : BindableBase
    {
        // プロパティ
        public ReactiveProperty<bool> IsShowMyPartyWindow { get; private set; }
        
        public ReactiveProperty<int> pokemonID1 { get; private set; }
        public ReactiveProperty<int> pokemonID2 { get; private set; }
        public ReactiveProperty<int> pokemonID3 { get; private set; }
        public ReactiveProperty<int> pokemonID4 { get; private set; }
        public ReactiveProperty<int> pokemonID5 { get; private set; }
        public ReactiveProperty<int> pokemonID6 { get; private set; }

        private ReactiveProperty<int>[] pokemonIdList;

        private BitmapSource _pokemonImage1;
        public BitmapSource PokemonImage1
        {
            get { return _pokemonImage1; }
            set { SetProperty(ref _pokemonImage1, value); }
        }

        private BitmapSource _frameImage1;
        public BitmapSource FrameImage1
        {
            get { return _frameImage1; }
            set { SetProperty(ref _frameImage1, value); }
        }


        //public ReactiveProperty<BitmapImage> PokemonImage1 { get; private set; }
        public ReactiveProperty<BitmapImage> PokemonImage2 { get; private set; }
        public ReactiveProperty<BitmapImage> PokemonImage3 { get; private set; }
        public ReactiveProperty<BitmapImage> PokemonImage4 { get; private set; }
        public ReactiveProperty<BitmapImage> PokemonImage5 { get; private set; }
        public ReactiveProperty<BitmapImage> PokemonImage6 { get; private set; }

        public ReactiveProperty<int[]> SelectedOrder { get; private set; }

        public ReactiveProperty<BitmapImage> Frame1 { get; private set; }

        // モデル
        private MyPartyWindowModel myPartyWindowModel;
        private MyPartyImageModel myPartyImageModel;

        // コマンド
        public DelegateCommand ChangeImageCommand { get; }

        public MyPartyWindowViewModel()
        {
            myPartyWindowModel = MyPartyWindowModel.GetInstance();
            myPartyImageModel = MyPartyImageModel.GetInstance();
            
            IsShowMyPartyWindow = myPartyWindowModel.ObserveProperty(m => m.IsShowWindow).ToReactiveProperty();

            //PokemonImage1 = myPartyImageModel.ObserveProperty(m => m.Pokemon1.Image).ToReactiveProperty();
            //PokemonImage2 = myPartyImageModel.ObserveProperty(m => m.Pokemon2.Image).ToReactiveProperty();

            //SelectedOrder = myPartyImageModel.ObserveProperty(m => m.Selected_Order).ToReactiveProperty();
            pokemonID1 = myPartyImageModel.ObserveProperty(m => m.PokemonID1).ToReactiveProperty();
            pokemonID1.Subscribe(id => ChangeImage(id));

            // ポケモンIDリスト
            pokemonIdList = new []{ pokemonID1, pokemonID2, pokemonID3, pokemonID4, pokemonID5, pokemonID6 };
            
            ChangeImageCommand = new DelegateCommand(ChangeImageCommandExecute);
        }

        // キャプチャ
        private void ChangeImageCommandExecute()
        {
            myPartyImageModel.PokemonID1 = 6;
        }

        private void ChangeImage(int PokemonId)
        {
            ImageModel test = new ImageModel();

            PokemonImage1 = test.createImage(pokemonID1.Value);
        }

        private string CreateImagePath()
        {
            String currentDir = Directory.GetCurrentDirectory();
            String imagePath = string.Format("Images/pokemon/{0}/icon.png", myPartyImageModel.Pokemon1.ID);
            String relativeImagePath = Path.Combine(currentDir, imagePath);

            if (File.Exists(relativeImagePath)) return relativeImagePath;

            return Path.Combine(currentDir, "Images/progress/progress3.png");
        }
    }
}
