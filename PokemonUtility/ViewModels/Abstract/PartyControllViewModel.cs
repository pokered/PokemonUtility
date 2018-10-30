using PokemonUtility.Models;
using PokemonUtility.Models.Notifications;
using PokemonUtility.Models.Party;
using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System.Reactive.Linq;
using System.Windows.Media.Imaging;

namespace PokemonUtility.ViewModels.Abstract
{
    class PartyControllViewModel : BindableBase
	{
        // ポケモンイメージ
        public ReactiveProperty<BitmapImage> PokemonImage0 { get; private set; }
        public ReactiveProperty<BitmapImage> PokemonImage1 { get; private set; }
        public ReactiveProperty<BitmapImage> PokemonImage2 { get; private set; }
        public ReactiveProperty<BitmapImage> PokemonImage3 { get; private set; }
        public ReactiveProperty<BitmapImage> PokemonImage4 { get; private set; }
        public ReactiveProperty<BitmapImage> PokemonImage5 { get; private set; }

        // サブウィンドウ表示リクエスト
        public InteractionRequest<INotification> ShowWindowRequest { get; } = new InteractionRequest<INotification>();

        // ポケモン検索ウィンドウ
        public DelegateCommand<object> ShowPokemonSearchWindowCommand { get; }

        // モデル
        protected PartyManegementModel _partyManegementModel;

        public PartyControllViewModel(PartyManegementModel partyManegementModel)
        {
            _partyManegementModel = partyManegementModel;

            // ポケモンIdの変更
            PokemonImage0 = _partyManegementModel
                .ObserveProperty(m => m.PokemonId0)
                .Select(x => ImageFactoryModel.CreatePokemonImage(x)).ToReactiveProperty();

            PokemonImage1 = _partyManegementModel
                .ObserveProperty(m => m.PokemonId1)
                .Select(x => ImageFactoryModel.CreatePokemonImage(x)).ToReactiveProperty();

            PokemonImage2 = _partyManegementModel
                .ObserveProperty(m => m.PokemonId2)
                .Select(x => ImageFactoryModel.CreatePokemonImage(x)).ToReactiveProperty();

            PokemonImage3 = _partyManegementModel
                .ObserveProperty(m => m.PokemonId3)
                .Select(x => ImageFactoryModel.CreatePokemonImage(x)).ToReactiveProperty();

            PokemonImage4 = _partyManegementModel
                .ObserveProperty(m => m.PokemonId4)
                .Select(x => ImageFactoryModel.CreatePokemonImage(x)).ToReactiveProperty();

            PokemonImage5 = _partyManegementModel
                .ObserveProperty(m => m.PokemonId5)
                .Select(x => ImageFactoryModel.CreatePokemonImage(x)).ToReactiveProperty();

            // コマンド
            ShowPokemonSearchWindowCommand = new DelegateCommand<object>(ShowPokemonSearchWindow);
        }

        private void ShowPokemonSearchWindow(object index)
        {
            // ポケモンId取得
            int partyIndex = ObjectConverter.ToInt(index);
            int pokemonId = _partyManegementModel.GetPokemonId(partyIndex);

            // 通知
            PokemonSearchWindowNotification pokemonSearchWindowNotification = new PokemonSearchWindowNotification();
            pokemonSearchWindowNotification.WindowId = PokemonSearchWindowNotification.POKEMON_SEARCH_WINDOW;
            pokemonSearchWindowNotification.IsModal = true;
            pokemonSearchWindowNotification.PokemonId = pokemonId;

            ShowWindowRequest.Raise(pokemonSearchWindowNotification, notification =>
            {
                PokemonSearchWindowNotification resultNotification = (PokemonSearchWindowNotification)notification;
                _partyManegementModel.ChangePokemonId(partyIndex, resultNotification.PokemonId);
            });
        }
    }
}
