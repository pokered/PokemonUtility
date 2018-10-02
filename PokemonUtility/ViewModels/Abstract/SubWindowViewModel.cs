using PokemonUtility.Models.Abstract;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace PokemonUtility.ViewModels.Abstract
{
    abstract class SubWindowViewModel : WindowViewModel
    {
        // ウィンドウ表示フラグ
        public ReactiveProperty<bool> IsShowWindow { get; private set; }

        public SubWindowViewModel(SubWindowModel subWindowModel) : base(subWindowModel)
        {
            // ウィンドウ表示フラグ紐づけ
            IsShowWindow = subWindowModel.ObserveProperty(m => m.IsShowWindow).ToReactiveProperty();
        }
    }
}
