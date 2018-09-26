using System;
using PokemonUtility.Const;
using Prism.Mvvm;
using Reactive.Bindings;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokemonUtility.Models.WaitState
{
    class PartyWaiStatetModel : BindableBase
    {
        //private bool _isAnalyzing1 = false;
        //public bool IsAnalyzing1 {
        //    get { return _isAnalyzing1; }
        //    set { SetProperty(ref _isAnalyzing1, value); }
        //}

        public ReactiveProperty<bool> IsAnalyzing1 { get; private set; } = new ReactiveProperty<bool>(false);
        public ReactiveProperty<bool> IsAnalyzing2 { get; private set; } = new ReactiveProperty<bool>(false);
        public ReactiveProperty<bool> IsAnalyzing3 { get; private set; } = new ReactiveProperty<bool>(false);
        public ReactiveProperty<bool> IsAnalyzing4 { get; private set; } = new ReactiveProperty<bool>(false);
        public ReactiveProperty<bool> IsAnalyzing5 { get; private set; } = new ReactiveProperty<bool>(false);
        public ReactiveProperty<bool> IsAnalyzing6 { get; private set; } = new ReactiveProperty<bool>(false);

        private int[] _waitStateList = new int[] {-1, -1, -1, -1, -1, -1 };
        public int WaitState1 {
            get { return _waitStateList[PartyConst.PARTY_INDEX1]; }
            set { SetProperty(ref _waitStateList[PartyConst.PARTY_INDEX1], value); }
        }

        public int WaitState2
        {
            get { return _waitStateList[PartyConst.PARTY_INDEX2]; }
            set { SetProperty(ref _waitStateList[PartyConst.PARTY_INDEX2], value); }
        }

        public int WaitState3
        {
            get { return _waitStateList[PartyConst.PARTY_INDEX3]; }
            set { SetProperty(ref _waitStateList[PartyConst.PARTY_INDEX3], value); }
        }

        public int WaitState4
        {
            get { return _waitStateList[PartyConst.PARTY_INDEX4]; }
            set { SetProperty(ref _waitStateList[PartyConst.PARTY_INDEX4], value); }
        }

        public int WaitState5
        {
            get { return _waitStateList[PartyConst.PARTY_INDEX5]; }
            set { SetProperty(ref _waitStateList[PartyConst.PARTY_INDEX5], value); }
        }

        public int WaitState6
        {
            get { return _waitStateList[PartyConst.PARTY_INDEX6]; }
            set { SetProperty(ref _waitStateList[PartyConst.PARTY_INDEX6], value); }
        }

        public PartyWaiStatetModel()
        {
            IsAnalyzing1.Subscribe(async _ => await Run());
        }

        public async Task Run()
        {
            while (IsAnalyzing1.Value)
            {
                WaitState1 = 0;
                await Task.Delay(300);
                WaitState1 = 1;
                await Task.Delay(300);
                WaitState1 = 2;
                await Task.Delay(300);
                WaitState1 = 3;
            }

            // 終了したら消す
            WaitState1 = -1;
        }

        public void Start()
        {
            IsAnalyzing1.Value = true;
        }

        public void End()
        {
            IsAnalyzing1.Value = false;
        }
    }
}
