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
        public ReactiveProperty<int> PokemonId0 { get; private set; }
        public ReactiveProperty<int> PokemonId1 { get; private set; }
        public ReactiveProperty<int> PokemonId2 { get; private set; }
        public ReactiveProperty<int> PokemonId3 { get; private set; }
        public ReactiveProperty<int> PokemonId4 { get; private set; }
        public ReactiveProperty<int> PokemonId5 { get; private set; }

        // 選出番号
        public ReactiveProperty<int> PokemonOrder0 { get; private set; }
        public ReactiveProperty<int> PokemonOrder1 { get; private set; }
        public ReactiveProperty<int> PokemonOrder2 { get; private set; }
        public ReactiveProperty<int> PokemonOrder3 { get; private set; }
        public ReactiveProperty<int> PokemonOrder4 { get; private set; }
        public ReactiveProperty<int> PokemonOrder5 { get; private set; }

        // 待機状態
        public ReactiveProperty<int> WaitState0 { get; private set; }
        public ReactiveProperty<int> WaitState1 { get; private set; }
        public ReactiveProperty<int> WaitState2 { get; private set; }
        public ReactiveProperty<int> WaitState3 { get; private set; }
        public ReactiveProperty<int> WaitState4 { get; private set; }
        public ReactiveProperty<int> WaitState5 { get; private set; }

        // ポケモンイメージ
        private BitmapImage[] _pokemonImageList = new BitmapImage[]
        {
            new BitmapImage(),
            new BitmapImage(),
            new BitmapImage(),
            new BitmapImage(),
            new BitmapImage(),
            new BitmapImage()
        };
        
        public BitmapImage PokemonImage0
        {
            get { return _pokemonImageList[PartyConst.PARTY_INDEX_FIRST]; }
            set { SetProperty(ref _pokemonImageList[PartyConst.PARTY_INDEX_FIRST], value); }
        }
        
        public BitmapImage PokemonImage1
        {
            get { return _pokemonImageList[PartyConst.PARTY_INDEX_SECOND]; }
            set { SetProperty(ref _pokemonImageList[PartyConst.PARTY_INDEX_SECOND], value); }
        }
        
        public BitmapImage PokemonImage2
        {
            get { return _pokemonImageList[PartyConst.PARTY_INDEX_THIRD]; }
            set { SetProperty(ref _pokemonImageList[PartyConst.PARTY_INDEX_THIRD], value); }
        }
        
        public BitmapImage PokemonImage3
        {
            get { return _pokemonImageList[PartyConst.PARTY_INDEX_FOUR]; }
            set { SetProperty(ref _pokemonImageList[PartyConst.PARTY_INDEX_FOUR], value); }
        }
        
        public BitmapImage PokemonImage4
        {
            get { return _pokemonImageList[PartyConst.PARTY_INDEX_FIFTH]; }
            set { SetProperty(ref _pokemonImageList[PartyConst.PARTY_INDEX_FIFTH], value); }
        }

        public BitmapImage PokemonImage5
        {
            get { return _pokemonImageList[PartyConst.PARTY_INDEX_SIXTH]; }
            set { SetProperty(ref _pokemonImageList[PartyConst.PARTY_INDEX_SIXTH], value); }
        }

        // フレーム
        private BitmapImage[] _frameImageList = new BitmapImage[]
        {
            new BitmapImage(),
            new BitmapImage(),
            new BitmapImage(),
            new BitmapImage(),
            new BitmapImage(),
            new BitmapImage()
        };
        
        public BitmapImage FrameImage0
        {
            get { return _frameImageList[PartyConst.PARTY_INDEX_FIRST]; }
            set { SetProperty(ref _frameImageList[PartyConst.PARTY_INDEX_FIRST], value); }
        }
        
        public BitmapImage FrameImage1
        {
            get { return _frameImageList[PartyConst.PARTY_INDEX_SECOND]; }
            set { SetProperty(ref _frameImageList[PartyConst.PARTY_INDEX_SECOND], value); }
        }
        
        public BitmapImage FrameImage2
        {
            get { return _frameImageList[PartyConst.PARTY_INDEX_THIRD]; }
            set { SetProperty(ref _frameImageList[PartyConst.PARTY_INDEX_THIRD], value); }
        }
        
        public BitmapImage FrameImage3
        {
            get { return _frameImageList[PartyConst.PARTY_INDEX_FOUR]; }
            set { SetProperty(ref _frameImageList[PartyConst.PARTY_INDEX_FOUR], value); }
        }
        
        public BitmapImage FrameImage4
        {
            get { return _frameImageList[PartyConst.PARTY_INDEX_FIFTH]; }
            set { SetProperty(ref _frameImageList[PartyConst.PARTY_INDEX_FIFTH], value); }
        }
        
        public BitmapImage FrameImage5
        {
            get { return _frameImageList[PartyConst.PARTY_INDEX_SIXTH]; }
            set { SetProperty(ref _frameImageList[PartyConst.PARTY_INDEX_SIXTH], value); }
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

        public BitmapImage WaitImage0
        {
            get { return _waitImageList[PartyConst.PARTY_INDEX_FIRST]; }
            set { SetProperty(ref _waitImageList[PartyConst.PARTY_INDEX_FIRST], value); }
        }

        public BitmapImage WaitImage1
        {
            get { return _waitImageList[PartyConst.PARTY_INDEX_SECOND]; }
            set { SetProperty(ref _waitImageList[PartyConst.PARTY_INDEX_SECOND], value); }
        }

        public BitmapImage WaitImage2
        {
            get { return _waitImageList[PartyConst.PARTY_INDEX_THIRD]; }
            set { SetProperty(ref _waitImageList[PartyConst.PARTY_INDEX_THIRD], value); }
        }

        public BitmapImage WaitImage3
        {
            get { return _waitImageList[PartyConst.PARTY_INDEX_FOUR]; }
            set { SetProperty(ref _waitImageList[PartyConst.PARTY_INDEX_FOUR], value); }
        }

        public BitmapImage WaitImage4
        {
            get { return _waitImageList[PartyConst.PARTY_INDEX_FIFTH]; }
            set { SetProperty(ref _waitImageList[PartyConst.PARTY_INDEX_FIFTH], value); }
        }

        public BitmapImage WaitImage5
        {
            get { return _waitImageList[PartyConst.PARTY_INDEX_SIXTH]; }
            set { SetProperty(ref _waitImageList[PartyConst.PARTY_INDEX_SIXTH], value); }
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
            WaitState0 = _partyWaitStateModel.ObserveProperty(m => m.WaitState0).ToReactiveProperty();
            WaitState1 = _partyWaitStateModel.ObserveProperty(m => m.WaitState1).ToReactiveProperty();
            WaitState2 = _partyWaitStateModel.ObserveProperty(m => m.WaitState2).ToReactiveProperty();
            WaitState3 = _partyWaitStateModel.ObserveProperty(m => m.WaitState3).ToReactiveProperty();
            WaitState4 = _partyWaitStateModel.ObserveProperty(m => m.WaitState4).ToReactiveProperty();
            WaitState5 = _partyWaitStateModel.ObserveProperty(m => m.WaitState5).ToReactiveProperty();

            // ポケモンIDプロパティ紐づけ
            PokemonId0 = _partyManegementModel.ObserveProperty(m => m.PokemonId0).ToReactiveProperty();
            PokemonId1 = _partyManegementModel.ObserveProperty(m => m.PokemonId1).ToReactiveProperty();
            PokemonId2 = _partyManegementModel.ObserveProperty(m => m.PokemonId2).ToReactiveProperty();
            PokemonId3 = _partyManegementModel.ObserveProperty(m => m.PokemonId3).ToReactiveProperty();
            PokemonId4 = _partyManegementModel.ObserveProperty(m => m.PokemonId4).ToReactiveProperty();
            PokemonId5 = _partyManegementModel.ObserveProperty(m => m.PokemonId5).ToReactiveProperty();

            // ポケモン選出順プロパティ紐づけ
            PokemonOrder0 = _partyManegementModel.ObserveProperty(m => m.PokemonOrder0).ToReactiveProperty();
            PokemonOrder1 = _partyManegementModel.ObserveProperty(m => m.PokemonOrder1).ToReactiveProperty();
            PokemonOrder2 = _partyManegementModel.ObserveProperty(m => m.PokemonOrder2).ToReactiveProperty();
            PokemonOrder3 = _partyManegementModel.ObserveProperty(m => m.PokemonOrder3).ToReactiveProperty();
            PokemonOrder4 = _partyManegementModel.ObserveProperty(m => m.PokemonOrder4).ToReactiveProperty();
            PokemonOrder5 = _partyManegementModel.ObserveProperty(m => m.PokemonOrder5).ToReactiveProperty();
            
            // ポケモンID変更時の処理登録
            PokemonId0.Subscribe(pokemonId => PokemonImage0 = ImageFactoryModel.CreatePokemonImage(pokemonId));
            PokemonId1.Subscribe(pokemonId => PokemonImage1 = ImageFactoryModel.CreatePokemonImage(pokemonId));
            PokemonId2.Subscribe(pokemonId => PokemonImage2 = ImageFactoryModel.CreatePokemonImage(pokemonId));
            PokemonId3.Subscribe(pokemonId => PokemonImage3 = ImageFactoryModel.CreatePokemonImage(pokemonId));
            PokemonId4.Subscribe(pokemonId => PokemonImage4 = ImageFactoryModel.CreatePokemonImage(pokemonId));
            PokemonId5.Subscribe(pokemonId => PokemonImage5 = ImageFactoryModel.CreatePokemonImage(pokemonId));

            // 選出番号変更時の処理登録
            PokemonOrder0.Subscribe(order => FrameImage0 = ImageFactoryModel.CreateFrameImage(PokemonId0.Value, order));
            PokemonOrder1.Subscribe(order => FrameImage1 = ImageFactoryModel.CreateFrameImage(PokemonId1.Value, order));
            PokemonOrder2.Subscribe(order => FrameImage2 = ImageFactoryModel.CreateFrameImage(PokemonId2.Value, order));
            PokemonOrder3.Subscribe(order => FrameImage3 = ImageFactoryModel.CreateFrameImage(PokemonId3.Value, order));
            PokemonOrder4.Subscribe(order => FrameImage4 = ImageFactoryModel.CreateFrameImage(PokemonId4.Value, order));
            PokemonOrder5.Subscribe(order => FrameImage5 = ImageFactoryModel.CreateFrameImage(PokemonId5.Value, order));

            // 待機状態変更時の処理登録
            WaitState0.Subscribe(waiteState => WaitImage0 = ImageFactoryModel.CreateProgressImage(waiteState));
            WaitState1.Subscribe(waiteState => WaitImage1 = ImageFactoryModel.CreateProgressImage(waiteState));
            WaitState2.Subscribe(waiteState => WaitImage2 = ImageFactoryModel.CreateProgressImage(waiteState));
            WaitState3.Subscribe(waiteState => WaitImage3 = ImageFactoryModel.CreateProgressImage(waiteState));
            WaitState4.Subscribe(waiteState => WaitImage4 = ImageFactoryModel.CreateProgressImage(waiteState));
            WaitState5.Subscribe(waiteState => WaitImage5 = ImageFactoryModel.CreateProgressImage(waiteState));
        }
    }
}
