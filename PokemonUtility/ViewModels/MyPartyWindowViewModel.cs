using PokemonUtility.Models;
using Prism.Mvvm;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace PokemonUtility.ViewModels
{
    public class MyPartyWindowViewModel : BindableBase
    {
        public ReactiveProperty<bool> WindowVisibility { get; private set; }
        public ReactiveProperty<string> Mess { get; private set; }

        // モデル
        private MyPartyWindowModel model;

        public MyPartyWindowViewModel()
        {
            model = MyPartyWindowModel.GetInstance();

            Mess = model.ObserveProperty(m => m.Mess).ToReactiveProperty();


            string aa = "bb";
            //WindowVisibility = model.ToReactivePropertyAsSynchronized(m => m.IsEnable);
        }
    }
}
