using PokemonUtility.Models;
using Prism.Mvvm;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace PokemonUtility.ViewModels
{
    public class MyPartyWindowViewModel : BindableBase
    {
        // プロパティ
        public ReactiveProperty<bool> IsShowMyPartyWindow { get; private set; }
        
        // モデル
        private MyPartyWindowModel myPartyWindowModel;
        
        public MyPartyWindowViewModel()
        {
            myPartyWindowModel = MyPartyWindowModel.GetInstance();

            IsShowMyPartyWindow = myPartyWindowModel.ObserveProperty(m => m.IsShowWindow).ToReactiveProperty();
        }
    }
}
