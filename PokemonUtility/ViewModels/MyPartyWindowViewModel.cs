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
        
        // ポケモンID
        public ReactiveProperty<int> pokemonId1 { get; private set; }
        public ReactiveProperty<int> pokemonId2 { get; private set; }
        public ReactiveProperty<int> pokemonId3 { get; private set; }
        public ReactiveProperty<int> pokemonId4 { get; private set; }
        public ReactiveProperty<int> pokemonId5 { get; private set; }
        public ReactiveProperty<int> pokemonId6 { get; private set; }

        // 選出番号
        public ReactiveProperty<int> OrderOfIndex1 { get; private set; }
        public ReactiveProperty<int> FrameId2 { get; private set; }
        public ReactiveProperty<int> FrameId3 { get; private set; }
        public ReactiveProperty<int> FrameId4 { get; private set; }
        public ReactiveProperty<int> FrameId5 { get; private set; }
        public ReactiveProperty<int> FrameId6 { get; private set; }

        private BitmapImage _pokemonImage1;
        public BitmapImage PokemonImage1
        {
            get { return _pokemonImage1; }
            set { SetProperty(ref _pokemonImage1, value); }
        }

        private BitmapImage _frameImage1;
        public BitmapImage FrameImage1
        {
            get { return _frameImage1; }
            set { SetProperty(ref _frameImage1, value); }
        }

        // モデル
        private MyPartyWindowModel myPartyWindowModel = MyPartyWindowModel.GetInstance();
        private MyPartyModel myPartyModel = MyPartyModel.GetInstance();

        // コマンド
        public DelegateCommand ChangeImageCommand { get; }

        public MyPartyWindowViewModel()
        {
            IsShowMyPartyWindow = myPartyWindowModel.ObserveProperty(m => m.IsShowWindow).ToReactiveProperty();

            pokemonId1 = myPartyModel.ObserveProperty(m => m.PokemonId1).ToReactiveProperty();
            pokemonId1.Subscribe(pokemonId => PokemonImage1 = ImageFactoryModel.CreatePokemonImage(pokemonId));

            OrderOfIndex1 = myPartyModel.ObserveProperty(m => m.OrderOfIndex1).ToReactiveProperty();
            OrderOfIndex1.Subscribe(order => FrameImage1 = ImageFactoryModel.CreateFrameImage(pokemonId1.Value, order));

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
