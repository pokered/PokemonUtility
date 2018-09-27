using System;
using PokemonUtility.Models;
using Prism.Commands;
using Prism.Mvvm;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System.Windows.Media.Imaging;
using PokemonUtility.Models.WaitState;
using PokemonUtility.Const;

namespace PokemonUtility.ViewModels
{
    class PartyWindowViewModel : BindableBase
    {
        // 位置
        public ReactiveProperty<int> X { get; private set; }
        public ReactiveProperty<int> Y { get; private set; }

        // ウィンドウ表示フラグ
        public ReactiveProperty<bool> IsShowPartyWindow { get; private set; }

        // ポケモンID
        public ReactiveProperty<int> PokemonId1 { get; private set; }
        public ReactiveProperty<int> PokemonId2 { get; private set; }
        public ReactiveProperty<int> PokemonId3 { get; private set; }
        public ReactiveProperty<int> PokemonId4 { get; private set; }
        public ReactiveProperty<int> PokemonId5 { get; private set; }
        public ReactiveProperty<int> PokemonId6 { get; private set; }

        // 選出番号
        public ReactiveProperty<int> PokemonOrder1 { get; private set; }
        public ReactiveProperty<int> PokemonOrder2 { get; private set; }
        public ReactiveProperty<int> PokemonOrder3 { get; private set; }
        public ReactiveProperty<int> PokemonOrder4 { get; private set; }
        public ReactiveProperty<int> PokemonOrder5 { get; private set; }
        public ReactiveProperty<int> PokemonOrder6 { get; private set; }

        // 待機状態
        public ReactiveProperty<int> WaitState1 { get; private set; }
        public ReactiveProperty<int> WaitState2 { get; private set; }
        public ReactiveProperty<int> WaitState3 { get; private set; }
        public ReactiveProperty<int> WaitState4 { get; private set; }
        public ReactiveProperty<int> WaitState5 { get; private set; }
        public ReactiveProperty<int> WaitState6 { get; private set; }

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

        // 待機イメージ
        private BitmapImage[] _waitImageList = new BitmapImage[]
        {
            new BitmapImage(),
            new BitmapImage(),
            new BitmapImage(),
            new BitmapImage(),
            new BitmapImage(),
            new BitmapImage()
        };

        public BitmapImage WaitImage1
        {
            get { return _waitImageList[PartyConst.PARTY_INDEX1]; }
            set { SetProperty(ref _waitImageList[PartyConst.PARTY_INDEX1], value); }
        }

        public BitmapImage WaitImage2
        {
            get { return _waitImageList[PartyConst.PARTY_INDEX2]; }
            set { SetProperty(ref _waitImageList[PartyConst.PARTY_INDEX2], value); }
        }

        public BitmapImage WaitImage3
        {
            get { return _waitImageList[PartyConst.PARTY_INDEX3]; }
            set { SetProperty(ref _waitImageList[PartyConst.PARTY_INDEX3], value); }
        }

        public BitmapImage WaitImage4
        {
            get { return _waitImageList[PartyConst.PARTY_INDEX4]; }
            set { SetProperty(ref _waitImageList[PartyConst.PARTY_INDEX4], value); }
        }

        public BitmapImage WaitImage5
        {
            get { return _waitImageList[PartyConst.PARTY_INDEX5]; }
            set { SetProperty(ref _waitImageList[PartyConst.PARTY_INDEX5], value); }
        }

        public BitmapImage WaitImage6
        {
            get { return _waitImageList[PartyConst.PARTY_INDEX6]; }
            set { SetProperty(ref _waitImageList[PartyConst.PARTY_INDEX6], value); }
        }

        // モデル
        protected PartyWindowModel _partyWindowModel;
        protected PartyWaiStatetModel _partyWaitStateModel;
        protected PartyManegementModel _partyManegementModel;
        
        // コマンド
        public DelegateCommand sanpleCommand { get; }
        
        public PartyWindowViewModel(
            PartyWindowModel partyWindowModel,
            PartyManegementModel partyManegementModel,
            PartyWaiStatetModel partyWaitStateModel
            )
        {
            // モデル設定
            _partyWindowModel = partyWindowModel;
            _partyWaitStateModel = partyWaitStateModel;
            _partyManegementModel = partyManegementModel;
            
            // ウィンドウ表示フラグ紐づけ
            IsShowPartyWindow = _partyWindowModel.ObserveProperty(m => m.IsShowWindow).ToReactiveProperty();
            
            // ウィンドウ位置紐づけ
            X = _partyWindowModel.ToReactivePropertyAsSynchronized(m => m.X);
            Y = _partyWindowModel.ToReactivePropertyAsSynchronized(m => m.Y);

            // 待機状態紐づけ
            WaitState1 = _partyWaitStateModel.ObserveProperty(m => m.WaitState1).ToReactiveProperty();
            WaitState2 = _partyWaitStateModel.ObserveProperty(m => m.WaitState2).ToReactiveProperty();
            WaitState3 = _partyWaitStateModel.ObserveProperty(m => m.WaitState3).ToReactiveProperty();
            WaitState4 = _partyWaitStateModel.ObserveProperty(m => m.WaitState4).ToReactiveProperty();
            WaitState5 = _partyWaitStateModel.ObserveProperty(m => m.WaitState5).ToReactiveProperty();
            WaitState6 = _partyWaitStateModel.ObserveProperty(m => m.WaitState6).ToReactiveProperty();

            // ポケモンIDプロパティ紐づけ
            PokemonId1 = _partyManegementModel.ObserveProperty(m => m.PokemonId1).ToReactiveProperty();
            PokemonId2 = _partyManegementModel.ObserveProperty(m => m.PokemonId2).ToReactiveProperty();
            PokemonId3 = _partyManegementModel.ObserveProperty(m => m.PokemonId3).ToReactiveProperty();
            PokemonId4 = _partyManegementModel.ObserveProperty(m => m.PokemonId4).ToReactiveProperty();
            PokemonId5 = _partyManegementModel.ObserveProperty(m => m.PokemonId5).ToReactiveProperty();
            PokemonId6 = _partyManegementModel.ObserveProperty(m => m.PokemonId6).ToReactiveProperty();

            // ポケモン選出順プロパティ紐づけ
            PokemonOrder1 = _partyManegementModel.ObserveProperty(m => m.PokemonOrder1).ToReactiveProperty();
            PokemonOrder2 = _partyManegementModel.ObserveProperty(m => m.PokemonOrder2).ToReactiveProperty();
            PokemonOrder3 = _partyManegementModel.ObserveProperty(m => m.PokemonOrder3).ToReactiveProperty();
            PokemonOrder4 = _partyManegementModel.ObserveProperty(m => m.PokemonOrder4).ToReactiveProperty();
            PokemonOrder5 = _partyManegementModel.ObserveProperty(m => m.PokemonOrder5).ToReactiveProperty();
            PokemonOrder6 = _partyManegementModel.ObserveProperty(m => m.PokemonOrder6).ToReactiveProperty();
            
            // ポケモンID変更時の処理登録
            PokemonId1.Subscribe(pokemonId => PokemonImage1 = ImageFactoryModel.CreatePokemonImage(pokemonId));
            PokemonId2.Subscribe(pokemonId => PokemonImage2 = ImageFactoryModel.CreatePokemonImage(pokemonId));
            PokemonId3.Subscribe(pokemonId => PokemonImage3 = ImageFactoryModel.CreatePokemonImage(pokemonId));
            PokemonId4.Subscribe(pokemonId => PokemonImage4 = ImageFactoryModel.CreatePokemonImage(pokemonId));
            PokemonId5.Subscribe(pokemonId => PokemonImage5 = ImageFactoryModel.CreatePokemonImage(pokemonId));
            PokemonId6.Subscribe(pokemonId => PokemonImage6 = ImageFactoryModel.CreatePokemonImage(pokemonId));

            // 選出番号変更時の処理登録
            PokemonOrder1.Subscribe(order => FrameImage1 = ImageFactoryModel.CreateFrameImage(PokemonId1.Value, order));
            PokemonOrder2.Subscribe(order => FrameImage2 = ImageFactoryModel.CreateFrameImage(PokemonId2.Value, order));
            PokemonOrder3.Subscribe(order => FrameImage3 = ImageFactoryModel.CreateFrameImage(PokemonId3.Value, order));
            PokemonOrder4.Subscribe(order => FrameImage4 = ImageFactoryModel.CreateFrameImage(PokemonId4.Value, order));
            PokemonOrder5.Subscribe(order => FrameImage5 = ImageFactoryModel.CreateFrameImage(PokemonId5.Value, order));
            PokemonOrder6.Subscribe(order => FrameImage6 = ImageFactoryModel.CreateFrameImage(PokemonId6.Value, order));

            // 待機状態変更時の処理登録
            WaitState1.Subscribe(waiteState => WaitImage1 = ImageFactoryModel.CreateProgressImage(waiteState));
            WaitState2.Subscribe(waiteState => WaitImage2 = ImageFactoryModel.CreateProgressImage(waiteState));
            WaitState3.Subscribe(waiteState => WaitImage3 = ImageFactoryModel.CreateProgressImage(waiteState));
            WaitState4.Subscribe(waiteState => WaitImage4 = ImageFactoryModel.CreateProgressImage(waiteState));
            WaitState5.Subscribe(waiteState => WaitImage5 = ImageFactoryModel.CreateProgressImage(waiteState));
            WaitState6.Subscribe(waiteState => WaitImage6 = ImageFactoryModel.CreateProgressImage(waiteState));
        }
    }
}
