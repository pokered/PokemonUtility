using PokemonUtility.Models;
using Prism.Commands;
using Prism.Mvvm;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System.Windows.Media.Imaging;
using System.Reactive.Linq;
using PokemonUtility.Models.Party;

namespace PokemonUtility.ViewModels
{
    class PartyWindowViewModel : BindableBase
    {
        // 位置
        public ReactiveProperty<int> X { get; private set; }
        public ReactiveProperty<int> Y { get; private set; }

        // ウィンドウ表示フラグ
        public ReactiveProperty<bool> IsShowWindow { get; private set; }

        // ウィンドウアクティブ
        public ReactiveProperty<bool> WindowEnabled { get; private set; }

        // ポケモンイメージ
        public ReactiveProperty<BitmapImage> PokemonImage0 { get; private set; }
        public ReactiveProperty<BitmapImage> PokemonImage1 { get; private set; }
        public ReactiveProperty<BitmapImage> PokemonImage2 { get; private set; }
        public ReactiveProperty<BitmapImage> PokemonImage3 { get; private set; }
        public ReactiveProperty<BitmapImage> PokemonImage4 { get; private set; }
        public ReactiveProperty<BitmapImage> PokemonImage5 { get; private set; }

        // フレームイメージ
        public ReactiveProperty<BitmapImage> FrameImage0 { get; private set; }
        public ReactiveProperty<BitmapImage> FrameImage1 { get; private set; }
        public ReactiveProperty<BitmapImage> FrameImage2 { get; private set; }
        public ReactiveProperty<BitmapImage> FrameImage3 { get; private set; }
        public ReactiveProperty<BitmapImage> FrameImage4 { get; private set; }
        public ReactiveProperty<BitmapImage> FrameImage5 { get; private set; }

        // 待機状態
        public ReactiveProperty<BitmapImage> WaitImage0 { get; private set; }
        public ReactiveProperty<BitmapImage> WaitImage1 { get; private set; }
        public ReactiveProperty<BitmapImage> WaitImage2 { get; private set; }
        public ReactiveProperty<BitmapImage> WaitImage3 { get; private set; }
        public ReactiveProperty<BitmapImage> WaitImage4 { get; private set; }
        public ReactiveProperty<BitmapImage> WaitImage5 { get; private set; }

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
            IsShowWindow = _partyWindowModel.ObserveProperty(m => m.IsShowWindow).ToReactiveProperty();

            // ウィンドウアクティブ紐づけ
            WindowEnabled = _partyWindowModel.ObserveProperty(m => m.IsShowWindow).ToReactiveProperty();

            // ウィンドウ位置紐づけ
            X = _partyWindowModel.ToReactivePropertyAsSynchronized(m => m.X);
            Y = _partyWindowModel.ToReactivePropertyAsSynchronized(m => m.Y);

            // ポケモンイメージ紐づけ
            PokemonImage0 = _partyManegementModel.ObserveProperty(m => m.PokemonId0).Select(x => ImageFactoryModel.CreatePokemonImage(x)).ToReactiveProperty();
            PokemonImage1 = _partyManegementModel.ObserveProperty(m => m.PokemonId1).Select(x => ImageFactoryModel.CreatePokemonImage(x)).ToReactiveProperty();
            PokemonImage2 = _partyManegementModel.ObserveProperty(m => m.PokemonId2).Select(x => ImageFactoryModel.CreatePokemonImage(x)).ToReactiveProperty();
            PokemonImage3 = _partyManegementModel.ObserveProperty(m => m.PokemonId3).Select(x => ImageFactoryModel.CreatePokemonImage(x)).ToReactiveProperty();
            PokemonImage4 = _partyManegementModel.ObserveProperty(m => m.PokemonId4).Select(x => ImageFactoryModel.CreatePokemonImage(x)).ToReactiveProperty();
            PokemonImage5 = _partyManegementModel.ObserveProperty(m => m.PokemonId5).Select(x => ImageFactoryModel.CreatePokemonImage(x)).ToReactiveProperty();

            // フレームイメージ紐づけ
            FrameImage0 = _partyManegementModel.ObserveProperty(m => m.PokemonOrder0).Select(x => ImageFactoryModel.CreateFrameImage(x)).ToReactiveProperty();
            FrameImage1 = _partyManegementModel.ObserveProperty(m => m.PokemonOrder1).Select(x => ImageFactoryModel.CreateFrameImage(x)).ToReactiveProperty();
            FrameImage2 = _partyManegementModel.ObserveProperty(m => m.PokemonOrder2).Select(x => ImageFactoryModel.CreateFrameImage(x)).ToReactiveProperty();
            FrameImage3 = _partyManegementModel.ObserveProperty(m => m.PokemonOrder3).Select(x => ImageFactoryModel.CreateFrameImage(x)).ToReactiveProperty();
            FrameImage4 = _partyManegementModel.ObserveProperty(m => m.PokemonOrder4).Select(x => ImageFactoryModel.CreateFrameImage(x)).ToReactiveProperty();
            FrameImage5 = _partyManegementModel.ObserveProperty(m => m.PokemonOrder5).Select(x => ImageFactoryModel.CreateFrameImage(x)).ToReactiveProperty();

            // 待機イメージ紐づけ
            WaitImage0 = _partyWaitStateModel.ObserveProperty(m => m.WaitState0).Select(x => ImageFactoryModel.CreateProgressImage(x)).ToReactiveProperty();
            WaitImage1 = _partyWaitStateModel.ObserveProperty(m => m.WaitState1).Select(x => ImageFactoryModel.CreateProgressImage(x)).ToReactiveProperty();
            WaitImage2 = _partyWaitStateModel.ObserveProperty(m => m.WaitState2).Select(x => ImageFactoryModel.CreateProgressImage(x)).ToReactiveProperty();
            WaitImage3 = _partyWaitStateModel.ObserveProperty(m => m.WaitState3).Select(x => ImageFactoryModel.CreateProgressImage(x)).ToReactiveProperty();
            WaitImage4 = _partyWaitStateModel.ObserveProperty(m => m.WaitState4).Select(x => ImageFactoryModel.CreateProgressImage(x)).ToReactiveProperty();
            WaitImage5 = _partyWaitStateModel.ObserveProperty(m => m.WaitState5).Select(x => ImageFactoryModel.CreateProgressImage(x)).ToReactiveProperty();

            sanpleCommand = new DelegateCommand(sample);
        }

        private void sample()
        {
            int aa = 0;
        }
    }

    class MyPartyWindowViewModel : PartyWindowViewModel
    {
        public MyPartyWindowViewModel() : base(MyPartyWindowModel.GetInstance(), MyPartyManegementModel.GetInstance(), MyPartyWaitStateModel.GetInstance())
        {
        }
    }

    class OpponentPartyWindowViewModel : PartyWindowViewModel
    {
        public OpponentPartyWindowViewModel() : base(OpponentPartyWindowModel.GetInstance(), OpponentPartyManegementModel.GetInstance(), OpponentPartyWaitStateModel.GetInstance())
        {
        }
    }
}
