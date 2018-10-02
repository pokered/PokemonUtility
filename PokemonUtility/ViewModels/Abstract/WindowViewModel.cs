using PokemonUtility.Models.Abstract;
using Prism.Mvvm;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace PokemonUtility.ViewModels.Abstract
{
    abstract class WindowViewModel : BindableBase
    {
        // ウィンドウ座標
        public ReactiveProperty<int> X { get; private set; }
        public ReactiveProperty<int> Y { get; private set; }

        // ウィンドウモデル
        protected WindowModel _windowModel;

        public WindowViewModel(WindowModel windowModel)
        {
            // モデル設定
            _windowModel = windowModel;

            // ウィンドウ座標紐づけ
            X = _windowModel.ToReactivePropertyAsSynchronized(m => m.X);
            Y = _windowModel.ToReactivePropertyAsSynchronized(m => m.Y);
        }
    }
}
