using PokemonUtility.Models;
using Prism.Commands;
using Prism.Mvvm;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.Windows.Media.Imaging;

namespace PokemonUtility.ViewModels
{
    public class MyPartyWindowViewModel : BindableBase
    {
        // ウィンドウ表示フラグ
        public ReactiveProperty<bool> IsShowMyPartyWindow { get; private set; }
        
        // ポケモンID
        public ReactiveProperty<int> pokemonId1 { get; private set; }
        public ReactiveProperty<int> pokemonId2 { get; private set; }
        public ReactiveProperty<int> pokemonId3 { get; private set; }
        public ReactiveProperty<int> pokemonId4 { get; private set; }
        public ReactiveProperty<int> pokemonId5 { get; private set; }
        public ReactiveProperty<int> pokemonId6 { get; private set; }

        // 選出番号
        public ReactiveProperty<int> PokemonOrder1 { get; private set; }
        public ReactiveProperty<int> PokemonOrder2 { get; private set; }
        public ReactiveProperty<int> PokemonOrder3 { get; private set; }
        public ReactiveProperty<int> PokemonOrder4 { get; private set; }
        public ReactiveProperty<int> PokemonOrder5 { get; private set; }
        public ReactiveProperty<int> PokemonOrder6 { get; private set; }

        // ポケモンイメージ
        private BitmapImage _pokemonImage1;
        public BitmapImage PokemonImage1
        {
            get { return _pokemonImage1; }
            set { SetProperty(ref _pokemonImage1, value); }
        }

        private BitmapImage _pokemonImage2;
        public BitmapImage PokemonImage2
        {
            get { return _pokemonImage2; }
            set { SetProperty(ref _pokemonImage2, value); }
        }

        private BitmapImage _pokemonImage3;
        public BitmapImage PokemonImage3
        {
            get { return _pokemonImage3; }
            set { SetProperty(ref _pokemonImage3, value); }
        }

        private BitmapImage _pokemonImage4;
        public BitmapImage PokemonImage4
        {
            get { return _pokemonImage4; }
            set { SetProperty(ref _pokemonImage4, value); }
        }

        private BitmapImage _pokemonImage5;
        public BitmapImage PokemonImage5
        {
            get { return _pokemonImage5; }
            set { SetProperty(ref _pokemonImage5, value); }
        }

        private BitmapImage _pokemonImage6;
        public BitmapImage PokemonImage6
        {
            get { return _pokemonImage6; }
            set { SetProperty(ref _pokemonImage6, value); }
        }

        // フレーム
        private BitmapImage _frameImage1;
        public BitmapImage FrameImage1
        {
            get { return _frameImage1; }
            set { SetProperty(ref _frameImage1, value); }
        }

        private BitmapImage _frameImage2;
        public BitmapImage FrameImage2
        {
            get { return _frameImage2; }
            set { SetProperty(ref _frameImage2, value); }
        }

        private BitmapImage _frameImage3;
        public BitmapImage FrameImage3
        {
            get { return _frameImage3; }
            set { SetProperty(ref _frameImage3, value); }
        }

        private BitmapImage _frameImage4;
        public BitmapImage FrameImage4
        {
            get { return _frameImage4; }
            set { SetProperty(ref _frameImage4, value); }
        }

        private BitmapImage _frameImage5;
        public BitmapImage FrameImage5
        {
            get { return _frameImage5; }
            set { SetProperty(ref _frameImage5, value); }
        }

        private BitmapImage _frameImage6;
        public BitmapImage FrameImage6
        {
            get { return _frameImage6; }
            set { SetProperty(ref _frameImage6, value); }
        }

        // モデル
        private MyPartyWindowModel myPartyWindowModel = MyPartyWindowModel.GetInstance();
        private MyPartyModel myPartyModel = MyPartyModel.GetInstance();

        // コマンド
        public DelegateCommand ChangeImageCommand { get; }

        public MyPartyWindowViewModel()
        {
            IsShowMyPartyWindow = myPartyWindowModel.ObserveProperty(m => m.IsShowWindow).ToReactiveProperty();

            // ポケモンIDプロパティ紐づけ
            pokemonId1 = myPartyModel.ObserveProperty(m => m.PokemonId1).ToReactiveProperty();
            pokemonId2 = myPartyModel.ObserveProperty(m => m.PokemonId2).ToReactiveProperty();
            pokemonId3 = myPartyModel.ObserveProperty(m => m.PokemonId3).ToReactiveProperty();
            pokemonId4 = myPartyModel.ObserveProperty(m => m.PokemonId4).ToReactiveProperty();
            pokemonId5 = myPartyModel.ObserveProperty(m => m.PokemonId5).ToReactiveProperty();
            pokemonId6 = myPartyModel.ObserveProperty(m => m.PokemonId6).ToReactiveProperty();

            // ポケモン選出順プロパティ紐づけ
            PokemonOrder1 = myPartyModel.ObserveProperty(m => m.PokemonOrder1).ToReactiveProperty();
            PokemonOrder2 = myPartyModel.ObserveProperty(m => m.PokemonOrder2).ToReactiveProperty();
            PokemonOrder3 = myPartyModel.ObserveProperty(m => m.PokemonOrder3).ToReactiveProperty();
            PokemonOrder4 = myPartyModel.ObserveProperty(m => m.PokemonOrder4).ToReactiveProperty();
            PokemonOrder5 = myPartyModel.ObserveProperty(m => m.PokemonOrder5).ToReactiveProperty();
            PokemonOrder6 = myPartyModel.ObserveProperty(m => m.PokemonOrder6).ToReactiveProperty();

            // ポケモンID変更時の処理登録
            pokemonId1.Subscribe(pokemonId => PokemonImage1 = ImageFactoryModel.CreatePokemonImage(pokemonId));
            //pokemonId1.Subscribe(pokemonId => FrameImage1 = ImageFactoryModel.CreateFrameImage(pokemonId, PokemonOrder1.Value));

            // 選出番号変更時の処理登録
            PokemonOrder1.Subscribe(order => FrameImage1 = ImageFactoryModel.CreateFrameImage(pokemonId1.Value, order));

            // コマンド
            ChangeImageCommand = new DelegateCommand(ChangeImageCommandExecute);
        }
        
        // キャプチャ
        private void ChangeImageCommandExecute()
        {
            pokemonId1.Value = 6;
        }
    }
}
