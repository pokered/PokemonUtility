using System;
using Prism.Mvvm;
using Reactive.Bindings;
using System.Threading.Tasks;

namespace PokemonUtility.Models.WaitState
{
    class WaitStateModel : BindableBase
    {
        private bool _isAnalyzing = false;
        public bool IsAnalyzing
        {
            get { return _isAnalyzing; }
            set { SetProperty(ref _isAnalyzing, value); }
        }

        //private ReactiveProperty<bool> IsAnalyzing { get; } = new ReactiveProperty<bool>(false);

        private int _state = -1;
        public int State
        {
            get { return _state; }
            set { SetProperty(ref _state, value); }
        }

        public WaitStateModel()
        {
            //IsAnalyzing.Subscribe(async _ => await Run());
        }

        // 待機状態を変更
        public async Task Run()
        {
            while (IsAnalyzing)
            {
                State = 0;
                await Task.Delay(300);
                State = 1;
                await Task.Delay(300);
                State = 2;
                await Task.Delay(300);
                State = 3;
            }

            // 終了したら消す
            State = -1;
        }

        public void Start()
        {
            IsAnalyzing = true;
        }

        public void End()
        {
            IsAnalyzing = false;
        }
    }
}
