using PokemonUtility.Models;
using PokemonUtility.Models.Common;
using PokemonUtility.Models.Search;
using PokemonUtility.Struct;
using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace PokemonUtility.ViewModels
{
    class PokemonSearchWindowViewModel : BindableBase
	{
        private List<Pokemon> _pokemons = new List<Pokemon>();
        public List<Pokemon> Pokemons
        {
            get { return _pokemons; }
            set { SetProperty(ref _pokemons, value); }
        }

        public ReactiveProperty<Pokemon> SelectedPokemon { get; } = new ReactiveProperty<Pokemon>();

        /// <summary>
        /// フィルターを実装するプロパティ
        /// </summary>
        public AutoCompleteFilterPredicate<object> PokemonFilter
        {
            get { return (searchText, obj) => (obj as Pokemon).Name.Contains(searchText); }
        }

        public ReactiveProperty<BitmapImage> PokemonImage { get; private set; } = new ReactiveProperty<BitmapImage>();

        public ReactiveProperty<string> PokemonName { get; set; } = new ReactiveProperty<string>();

        // サブウィンドウ表示リクエスト
        public InteractionRequest<INotification> CloseWindowRequest { get; } = new InteractionRequest<INotification>();

        // 分析コマンド
        public DelegateCommand<object> CloseWindowCommand { get; }

        // モデル
        public PokemonSearchWindowModel PokemonSearchWindowModel { get; } = ModelConnector.PokemonSearchWindow;

        public PokemonSearchWindowViewModel()
        {
            // 入力補完候補を設定
            PokemonDataModel pokemonDataModel = new PokemonDataModel();
            Pokemons = pokemonDataModel.GetPokemons();

            // 紐づけ
            PokemonImage = PokemonSearchWindowModel
                .ObserveProperty(m => m.PokemonId)
                .Select(x => ImageFactoryModel.CreatePokemonImage(x)).ToReactiveProperty();

            // 処理
            PokemonName.Subscribe(pokemonName => PokemonSearchWindowModel.ChangePokemonId(pokemonName));

            // コマンド
            CloseWindowCommand = new DelegateCommand<object>(CloseWindow);
        }

        private void CloseWindow(object isChangePokemonId)
        {
            ModelConnector.PokemonSearchWindow.IsChangePokemonId = ObjectConverter.ToBoolean(isChangePokemonId);
            CloseWindowRequest.Raise(new Notification());
        }
    }
}
