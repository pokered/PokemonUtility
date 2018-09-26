using System;
using Prism.Mvvm;
using Reactive.Bindings;
using System.Threading.Tasks;

namespace PokemonUtility.Models.WaitState
{
    class WaitStateModel : BindableBase
    {
        //private bool _isAnalyzing = false;
        //public bool IsAnalyzing
        //{
        //    get { return _isAnalyzing; }
        //    set { SetProperty(ref _isAnalyzing, value); }
        //}

        private ReactiveProperty<bool> IsAnalyzing { get; }

        private int waitState = -1;
        public int WaitState
        {
            get { return waitState; }
            set { SetProperty(ref waitState, value); }
        }

        public WaitStateModel()
        {
            IsAnalyzing.Value = false;
            IsAnalyzing.Subscribe(async _ => await Run());
        }

        // 待機状態を変更
        private async Task Run()
        {
            while (IsAnalyzing.Value)
            {
                WaitState = 0;
                await Task.Delay(300);
                WaitState = 1;
                await Task.Delay(300);
                WaitState = 2;
                await Task.Delay(300);
                WaitState = 3;
            }

            // 終了したら消す
            WaitState = -1;
        }

        public void Start()
        {
            IsAnalyzing.Value = true;
        }

        public void End()
        {
            IsAnalyzing.Value = false;
        }
    }
}
