using PokemonUtility.Models;
using Prism.Commands;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System.Windows.Media.Imaging;
using System.Reactive.Linq;
using PokemonUtility.Models.Party;
using PokemonUtility.ViewModels.Abstract;

namespace PokemonUtility.ViewModels
{
    class PartyWindowViewModel : SubWindowViewModel
    {
        // ウィンドウアクティブ
        public ReactiveProperty<bool> WindowEnabled { get; private set; }

        // ポケモンイメージ
        public ReactiveProperty<BitmapImage> PokemonImage0 { get; private set; }
        public ReactiveProperty<BitmapImage> PokemonImage1 { get; private set; }
        public ReactiveProperty<BitmapImage> PokemonImage2 { get; private set; }
        public ReactiveProperty<BitmapImage> PokemonImage3 { get; private set; }
        public ReactiveProperty<BitmapImage> PokemonImage4 { get; private set; }
        public ReactiveProperty<BitmapImage> PokemonImage5 { get; private set; }

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
            ) : base(partyWindowModel)
        {
            // モデル設定
            _partyWindowModel = partyWindowModel;
            _partyWaitStateModel = partyWaitStateModel;
            _partyManegementModel = partyManegementModel;
            
            // ウィンドウアクティブ紐づけ
            WindowEnabled = _partyWindowModel.ObserveProperty(m => m.IsAnalyzing).Select(x => !x).ToReactiveProperty();

            // ポケモンIdの変更
            PokemonImage0 = _partyManegementModel
                .ObserveProperty(m => m.PokemonId0)
                .Select(x => ImageFactoryModel.PokemonImageAddFrameImage(x, _partyManegementModel.PokemonOrder0)).ToReactiveProperty();

            PokemonImage1 = _partyManegementModel
                .ObserveProperty(m => m.PokemonId1)
                .Select(x => ImageFactoryModel.PokemonImageAddFrameImage(x, _partyManegementModel.PokemonOrder1)).ToReactiveProperty();

            PokemonImage2 = _partyManegementModel
                .ObserveProperty(m => m.PokemonId2)
                .Select(x => ImageFactoryModel.PokemonImageAddFrameImage(x, _partyManegementModel.PokemonOrder2)).ToReactiveProperty();

            PokemonImage3 = _partyManegementModel
                .ObserveProperty(m => m.PokemonId3)
                .Select(x => ImageFactoryModel.PokemonImageAddFrameImage(x, _partyManegementModel.PokemonOrder3)).ToReactiveProperty();

            PokemonImage4 = _partyManegementModel
                .ObserveProperty(m => m.PokemonId4)
                .Select(x => ImageFactoryModel.PokemonImageAddFrameImage(x, _partyManegementModel.PokemonOrder4)).ToReactiveProperty();

            PokemonImage5 = _partyManegementModel
                .ObserveProperty(m => m.PokemonId5)
                .Select(x => ImageFactoryModel.PokemonImageAddFrameImage(x, _partyManegementModel.PokemonOrder5)).ToReactiveProperty();

            // オーダー変更
            PokemonImage0 = _partyManegementModel
                .ObserveProperty(m => m.PokemonOrder0)
                .Select(x => ImageFactoryModel.PokemonImageAddFrameImage(_partyManegementModel.PokemonId0, x)).ToReactiveProperty();

            PokemonImage1 = _partyManegementModel
                .ObserveProperty(m => m.PokemonOrder1)
                .Select(x => ImageFactoryModel.PokemonImageAddFrameImage(_partyManegementModel.PokemonId1, x)).ToReactiveProperty();

            PokemonImage2 = _partyManegementModel
                .ObserveProperty(m => m.PokemonOrder2)
                .Select(x => ImageFactoryModel.PokemonImageAddFrameImage(_partyManegementModel.PokemonId2, x)).ToReactiveProperty();

            PokemonImage3 = _partyManegementModel
                .ObserveProperty(m => m.PokemonOrder3)
                .Select(x => ImageFactoryModel.PokemonImageAddFrameImage(_partyManegementModel.PokemonId3, x)).ToReactiveProperty();

            PokemonImage4 = _partyManegementModel
                .ObserveProperty(m => m.PokemonOrder4)
                .Select(x => ImageFactoryModel.PokemonImageAddFrameImage(_partyManegementModel.PokemonId4, x)).ToReactiveProperty();

            PokemonImage5 = _partyManegementModel
                .ObserveProperty(m => m.PokemonOrder5)
                .Select(x => ImageFactoryModel.PokemonImageAddFrameImage(_partyManegementModel.PokemonId5, x, 40)).ToReactiveProperty();

            // 待機イメージ紐づけ
            WaitImage0 = _partyWaitStateModel.ObserveProperty(m => m.WaitState0).Select(x => ImageFactoryModel.CreateProgressImage(x)).ToReactiveProperty();
            WaitImage1 = _partyWaitStateModel.ObserveProperty(m => m.WaitState1).Select(x => ImageFactoryModel.CreateProgressImage(x)).ToReactiveProperty();
            WaitImage2 = _partyWaitStateModel.ObserveProperty(m => m.WaitState2).Select(x => ImageFactoryModel.CreateProgressImage(x)).ToReactiveProperty();
            WaitImage3 = _partyWaitStateModel.ObserveProperty(m => m.WaitState3).Select(x => ImageFactoryModel.CreateProgressImage(x)).ToReactiveProperty();
            WaitImage4 = _partyWaitStateModel.ObserveProperty(m => m.WaitState4).Select(x => ImageFactoryModel.CreateProgressImage(x)).ToReactiveProperty();
            WaitImage5 = _partyWaitStateModel.ObserveProperty(m => m.WaitState5).Select(x => ImageFactoryModel.CreateProgressImage(x)).ToReactiveProperty();
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
