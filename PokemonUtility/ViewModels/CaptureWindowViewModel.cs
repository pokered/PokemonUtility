using PokemonUtility.Models;
using Prism.Commands;
using Prism.Mvvm;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace PokemonUtility.ViewModels
{
    public class CaptureWindowViewModel : BindableBase
    {
        // 位置
        public ReactiveProperty<int> X { get; private set; }
        public ReactiveProperty<int> Y { get; private set; }
        public ReactiveProperty<int> Width { get; private set; }
        public ReactiveProperty<int> Height { get; private set; }

        // モデル
        private CaptureWindowModel _captureWindowModel = CaptureWindowModel.GetInstance();

        // コマンド
        public DelegateCommand CloseWindowCommand { get; }

        public CaptureWindowViewModel()
        {
            // ウィンドウ情報紐づけ
            X = _captureWindowModel.ToReactivePropertyAsSynchronized(m => m.X);
            Y = _captureWindowModel.ToReactivePropertyAsSynchronized(m => m.Y);
            Width = _captureWindowModel.ToReactivePropertyAsSynchronized(m => m.Width);
            Height = _captureWindowModel.ToReactivePropertyAsSynchronized(m => m.Height);
        }
    }
}
