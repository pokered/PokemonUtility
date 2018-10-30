using PokemonUtility.Models;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System.Windows.Media.Imaging;
using System.Reactive.Linq;
using PokemonUtility.Models.Party;
using PokemonUtility.ViewModels.Abstract;
using System;
using Prism.Interactivity.InteractionRequest;

namespace PokemonUtility.ViewModels.Abstract
{
    class PartyWindowViewModel : SubWindowViewModel
    {
        // ウィンドウアクティブ
        public ReactiveProperty<bool> WindowEnabled { get; private set; }

        // ポケモンID
        public ReactiveProperty<int> PokemonId0 { get; }
        public ReactiveProperty<int> PokemonId1 { get; }
        public ReactiveProperty<int> PokemonId2 { get; }
        public ReactiveProperty<int> PokemonId3 { get; }
        public ReactiveProperty<int> PokemonId4 { get; }
        public ReactiveProperty<int> PokemonId5 { get; }

        // オーダー
        public ReactiveProperty<int> PokemonOrder0 { get; }
        public ReactiveProperty<int> PokemonOrder1 { get; }
        public ReactiveProperty<int> PokemonOrder2 { get; }
        public ReactiveProperty<int> PokemonOrder3 { get; }
        public ReactiveProperty<int> PokemonOrder4 { get; }
        public ReactiveProperty<int> PokemonOrder5 { get; }

        // ポケモンイメージ
        public ReactiveProperty<BitmapImage> PokemonImage0 { get; }
        public ReactiveProperty<BitmapImage> PokemonImage1 { get; }
        public ReactiveProperty<BitmapImage> PokemonImage2 { get; }
        public ReactiveProperty<BitmapImage> PokemonImage3 { get; }
        public ReactiveProperty<BitmapImage> PokemonImage4 { get; }
        public ReactiveProperty<BitmapImage> PokemonImage5 { get; }

        // 待機状態
        public ReactiveProperty<BitmapImage> WaitImage0 { get; private set; }
        public ReactiveProperty<BitmapImage> WaitImage1 { get; private set; }
        public ReactiveProperty<BitmapImage> WaitImage2 { get; private set; }
        public ReactiveProperty<BitmapImage> WaitImage3 { get; private set; }
        public ReactiveProperty<BitmapImage> WaitImage4 { get; private set; }
        public ReactiveProperty<BitmapImage> WaitImage5 { get; private set; }

        // リクエスト
        public InteractionRequest<INotification> CloseWindowRequest { get; } = new InteractionRequest<INotification>();

        public PartyWindowViewModel(
            PartyWindowModel partyWindowModel,
            PartyManegementModel partyManegementModel,
            PartyWaitStateModel partyWaitStateModel
            ) : base(partyWindowModel)
        {
            // ウィンドウクローズ
            IsShowWindow.Where(x => !x).Subscribe(_ => CloseWindowRequest.Raise(new Notification()));
            
            // ウィンドウアクティブ紐づけ
            WindowEnabled = ModelConnector.Analysis.ObserveProperty(m => m.IsAnalyzing).Select(x => !x).ToReactiveProperty();

            PokemonId0 = partyManegementModel.ObserveProperty(m => m.PokemonId0).ToReactiveProperty();
            PokemonId1 = partyManegementModel.ObserveProperty(m => m.PokemonId1).ToReactiveProperty();
            PokemonId2 = partyManegementModel.ObserveProperty(m => m.PokemonId2).ToReactiveProperty();
            PokemonId3 = partyManegementModel.ObserveProperty(m => m.PokemonId3).ToReactiveProperty();
            PokemonId4 = partyManegementModel.ObserveProperty(m => m.PokemonId4).ToReactiveProperty();
            PokemonId5 = partyManegementModel.ObserveProperty(m => m.PokemonId5).ToReactiveProperty();

            PokemonOrder0 = partyManegementModel.ObserveProperty(m => m.PokemonOrder0).ToReactiveProperty();
            PokemonOrder1 = partyManegementModel.ObserveProperty(m => m.PokemonOrder1).ToReactiveProperty();
            PokemonOrder2 = partyManegementModel.ObserveProperty(m => m.PokemonOrder2).ToReactiveProperty();
            PokemonOrder3 = partyManegementModel.ObserveProperty(m => m.PokemonOrder3).ToReactiveProperty();
            PokemonOrder4 = partyManegementModel.ObserveProperty(m => m.PokemonOrder4).ToReactiveProperty();
            PokemonOrder5 = partyManegementModel.ObserveProperty(m => m.PokemonOrder5).ToReactiveProperty();

            PokemonImage0 = PokemonId0
                .CombineLatest(PokemonOrder0, (pokemonId, order) => ImageFactoryModel.CreatePokemonImage(pokemonId, order)).ToReactiveProperty();

            PokemonImage1 = PokemonId1
                .CombineLatest(PokemonOrder1, (pokemonId, order) => ImageFactoryModel.CreatePokemonImage(pokemonId, order)).ToReactiveProperty();

            PokemonImage2 = PokemonId2
                .CombineLatest(PokemonOrder2, (pokemonId, order) => ImageFactoryModel.CreatePokemonImage(pokemonId, order)).ToReactiveProperty();

            PokemonImage3 = PokemonId3
                .CombineLatest(PokemonOrder3, (pokemonId, order) => ImageFactoryModel.CreatePokemonImage(pokemonId, order)).ToReactiveProperty();

            PokemonImage4 = PokemonId4
                .CombineLatest(PokemonOrder4, (pokemonId, order) => ImageFactoryModel.CreatePokemonImage(pokemonId, order)).ToReactiveProperty();

            PokemonImage5 = PokemonId5
                .CombineLatest(PokemonOrder5, (pokemonId, order) => ImageFactoryModel.CreatePokemonImage(pokemonId, order)).ToReactiveProperty();

            // 待機イメージ紐づけ
            WaitImage0 = partyWaitStateModel.ObserveProperty(m => m.WaitState0).Select(x => ImageFactoryModel.CreateWaitImage(x)).ToReactiveProperty();
            WaitImage1 = partyWaitStateModel.ObserveProperty(m => m.WaitState1).Select(x => ImageFactoryModel.CreateWaitImage(x)).ToReactiveProperty();
            WaitImage2 = partyWaitStateModel.ObserveProperty(m => m.WaitState2).Select(x => ImageFactoryModel.CreateWaitImage(x)).ToReactiveProperty();
            WaitImage3 = partyWaitStateModel.ObserveProperty(m => m.WaitState3).Select(x => ImageFactoryModel.CreateWaitImage(x)).ToReactiveProperty();
            WaitImage4 = partyWaitStateModel.ObserveProperty(m => m.WaitState4).Select(x => ImageFactoryModel.CreateWaitImage(x)).ToReactiveProperty();
            WaitImage5 = partyWaitStateModel.ObserveProperty(m => m.WaitState5).Select(x => ImageFactoryModel.CreateWaitImage(x)).ToReactiveProperty();
        }
    }


    class OpponentPartyWindowViewModel : PartyWindowViewModel
    {
        public OpponentPartyWindowViewModel() : base(OpponentPartyWindowModel.GetInstance(), OpponentPartyManegementModel.GetInstance(), OpponentPartyWaitStateModel.GetInstance())
        {
        }
    }
}
