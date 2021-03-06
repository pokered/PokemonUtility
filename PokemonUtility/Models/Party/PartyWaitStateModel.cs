﻿using System;
using PokemonUtility.Const;
using Prism.Mvvm;
using Reactive.Bindings;
using System.Threading.Tasks;
using System.Reactive.Linq;

namespace PokemonUtility.Models.Party
{
    class PartyWaitStateModel : BindableBase
    {
        // プロパティ
        public ReactiveProperty<bool> IsAnalyzing0 { get; private set; } = new ReactiveProperty<bool>();
        public ReactiveProperty<bool> IsAnalyzing1 { get; private set; } = new ReactiveProperty<bool>();
        public ReactiveProperty<bool> IsAnalyzing2 { get; private set; } = new ReactiveProperty<bool>();
        public ReactiveProperty<bool> IsAnalyzing3 { get; private set; } = new ReactiveProperty<bool>();
        public ReactiveProperty<bool> IsAnalyzing4 { get; private set; } = new ReactiveProperty<bool>();
        public ReactiveProperty<bool> IsAnalyzing5 { get; private set; } = new ReactiveProperty<bool>();

        // 待機状態
        private int[] _waitStateList = new int[] { -1, -1, -1, -1, -1, -1 };

        public int WaitState0
        {
            get { return _waitStateList[PartyConst.FIRST]; }
            set { SetProperty(ref _waitStateList[PartyConst.FIRST], value); }
        }

        public int WaitState1
        {
            get { return _waitStateList[PartyConst.SECOND]; }
            set { SetProperty(ref _waitStateList[PartyConst.SECOND], value); }
        }

        public int WaitState2
        {
            get { return _waitStateList[PartyConst.THIRD]; }
            set { SetProperty(ref _waitStateList[PartyConst.THIRD], value); }
        }

        public int WaitState3
        {
            get { return _waitStateList[PartyConst.FOURTH]; }
            set { SetProperty(ref _waitStateList[PartyConst.FOURTH], value); }
        }

        public int WaitState4
        {
            get { return _waitStateList[PartyConst.FIFTH]; }
            set { SetProperty(ref _waitStateList[PartyConst.FIFTH], value); }
        }

        public int WaitState5
        {
            get { return _waitStateList[PartyConst.SIXTH]; }
            set { SetProperty(ref _waitStateList[PartyConst.SIXTH], value); }
        }

        public PartyWaitStateModel()
        {
            IsAnalyzing0.Where(x => x).Subscribe(async _ => await Run(PartyConst.FIRST));
            IsAnalyzing1.Where(x => x).Subscribe(async _ => await Run(PartyConst.SECOND));
            IsAnalyzing2.Where(x => x).Subscribe(async _ => await Run(PartyConst.THIRD));
            IsAnalyzing3.Where(x => x).Subscribe(async _ => await Run(PartyConst.FOURTH));
            IsAnalyzing4.Where(x => x).Subscribe(async _ => await Run(PartyConst.FIFTH));
            IsAnalyzing5.Where(x => x).Subscribe(async _ => await Run(PartyConst.SIXTH));
        }

        private async Task Run(int partyIndex)
        {
            ReactiveProperty<bool> isAnalyzing = GetIsAnalyzingProperty(partyIndex);

            // 待機状態プロパティ情報取得
            var property = GetType().GetProperty(string.Format("WaitState{0}", partyIndex));

            while (isAnalyzing.Value)
            {
                property.SetValue(this, WaitStateConst.FIRST_STEP);
                await Task.Delay(200);
                property.SetValue(this, WaitStateConst.SECOND_STEP);
                await Task.Delay(200);
                property.SetValue(this, WaitStateConst.THIRD_STEP);
                await Task.Delay(200);
                property.SetValue(this, WaitStateConst.FOUR_STEP);
                await Task.Delay(200);
            }

            // 終了したら消す
            property.SetValue(this, WaitStateConst.END);
        }

        public void Start(int partyIndex)
        {
            // プロパティの値を取得
            ReactiveProperty<bool> isAnalyzing = GetIsAnalyzingProperty(partyIndex);

            isAnalyzing.Value = true;
        }

        public async Task<bool> End(int partyIndex)
        {
            // プロパティの値を取得
            ReactiveProperty<bool> isAnalyzing = GetIsAnalyzingProperty(partyIndex);

            // 分析終了
            isAnalyzing.Value = false;

            // 待機演出が終わるまで待つ
            while (GetWaitStateProperty(partyIndex) != WaitStateConst.END)
            {
                await Task.Delay(100);
            }

            // 終了通知
            return true;
        }
        
        private ReactiveProperty<bool> GetIsAnalyzingProperty(int partyIndex)
        {
            // プロパティ情報取得
            var property = GetType().GetProperty(string.Format("IsAnalyzing{0}", partyIndex));

            // プロパティが存在しなければ終了
            if (property == null) return new ReactiveProperty<bool>(false);

            // プロパティの値を取得
            return (ReactiveProperty<bool>)property.GetValue(this);
        }

        private int GetWaitStateProperty(int partyIndex)
        {
            // プロパティ情報取得
            var property = GetType().GetProperty(string.Format("WaitState{0}", partyIndex));

            // プロパティが存在しなければ終了
            if (property == null) return WaitStateConst.FOUR_STEP;

            // プロパティの値を取得
            return ObjectConverter.ToInt(property.GetValue(this));
        }
    }

    class MyPartyWaitStateModel : PartyWaitStateModel
    {
        #region Singleton

        static MyPartyWaitStateModel Instance;
        public static MyPartyWaitStateModel GetInstance()
        {
            if (Instance == null)
                Instance = new MyPartyWaitStateModel();
            return Instance;
        }

        #endregion
    }

    class OpponentPartyWaitStateModel : PartyWaitStateModel
    {
        #region Singleton

        static OpponentPartyWaitStateModel Instance;
        public static OpponentPartyWaitStateModel GetInstance()
        {
            if (Instance == null)
                Instance = new OpponentPartyWaitStateModel();
            return Instance;
        }

        #endregion
    }
}
