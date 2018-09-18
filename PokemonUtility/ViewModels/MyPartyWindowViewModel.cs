﻿using PokemonUtility.Models;
using Prism.Mvvm;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System.Windows.Media.Imaging;

namespace PokemonUtility.ViewModels
{
    public class MyPartyWindowViewModel : BindableBase
    {
        // プロパティ
        public ReactiveProperty<bool> IsShowMyPartyWindow { get; private set; }

        public ReactiveProperty<BitmapImage> PokemonImage1 { get; private set; }
        public ReactiveProperty<BitmapImage> PokemonImage2 { get; private set; }
        public ReactiveProperty<BitmapImage> PokemonImage3 { get; private set; }
        public ReactiveProperty<BitmapImage> PokemonImage4 { get; private set; }
        public ReactiveProperty<BitmapImage> PokemonImage5 { get; private set; }
        public ReactiveProperty<BitmapImage> PokemonImage6 { get; private set; }

        public ReactiveProperty<BitmapImage> Frame1 { get; private set; }

        // モデル
        private MyPartyWindowModel myPartyWindowModel;
        private MyPartyImageModel myPartyImageModel;
        
        public MyPartyWindowViewModel()
        {
            myPartyWindowModel = MyPartyWindowModel.GetInstance();
            myPartyImageModel = MyPartyImageModel.GetInstance();
            
            IsShowMyPartyWindow = myPartyWindowModel.ObserveProperty(m => m.IsShowWindow).ToReactiveProperty();

            PokemonImage1 = myPartyImageModel.ObserveProperty(m => m.Pokemon1.Image).ToReactiveProperty();
            //PokemonImage2 = myPartyImageModel.ObserveProperty(m => m.Pokemon2.Image).ToReactiveProperty();

            Frame1 = myPartyImageModel.ObserveProperty(m => m.Frame).ToReactiveProperty();
            myPartyImageModel.Pokemon1.ID = 1;
        }
    }
}
