using System;
using PokemonUtility.Const;
using Prism.Mvvm;
using Reactive.Bindings;
using System.Threading.Tasks;
using System.Reactive.Linq;

namespace PokemonUtility.Models.WaitState
{
    class PartyWaiStatetModel : BindableBase
    {
        public ReactiveProperty<bool> IsAnalyzing0 { get; private set; } = new ReactiveProperty<bool>();
        public ReactiveProperty<bool> IsAnalyzing1 { get; private set; } = new ReactiveProperty<bool>();
        public ReactiveProperty<bool> IsAnalyzing2 { get; private set; } = new ReactiveProperty<bool>();
        public ReactiveProperty<bool> IsAnalyzing3 { get; private set; } = new ReactiveProperty<bool>();
        public ReactiveProperty<bool> IsAnalyzing4 { get; private set; } = new ReactiveProperty<bool>();
        public ReactiveProperty<bool> IsAnalyzing5 { get; private set; } = new ReactiveProperty<bool>();

        private int[] _waitStateList = new int[] {-1, -1, -1, -1, -1, -1 };
        public int WaitState0 {
            get { return _waitStateList[PartyConst.PARTY_INDEX_FIRST]; }
            set { SetProperty(ref _waitStateList[PartyConst.PARTY_INDEX_FIRST], value); }
        }

        public int WaitState1
        {
            get { return _waitStateList[PartyConst.PARTY_INDEX_SECOND]; }
            set { SetProperty(ref _waitStateList[PartyConst.PARTY_INDEX_SECOND], value); }
        }

        public int WaitState2
        {
            get { return _waitStateList[PartyConst.PARTY_INDEX_THIRD]; }
            set { SetProperty(ref _waitStateList[PartyConst.PARTY_INDEX_THIRD], value); }
        }

        public int WaitState3
        {
            get { return _waitStateList[PartyConst.PARTY_INDEX_FOUR]; }
            set { SetProperty(ref _waitStateList[PartyConst.PARTY_INDEX_FOUR], value); }
        }

        public int WaitState4
        {
            get { return _waitStateList[PartyConst.PARTY_INDEX_FIFTH]; }
            set { SetProperty(ref _waitStateList[PartyConst.PARTY_INDEX_FIFTH], value); }
        }

        public int WaitState5
        {
            get { return _waitStateList[PartyConst.PARTY_INDEX_SIXTH]; }
            set { SetProperty(ref _waitStateList[PartyConst.PARTY_INDEX_SIXTH], value); }
        }

        public PartyWaiStatetModel()
        {
            IsAnalyzing0.Select(x => x).Subscribe(async _ => await Run(PartyConst.PARTY_INDEX_FIRST));
            IsAnalyzing1.Select(x => x).Subscribe(async _ => await Run(PartyConst.PARTY_INDEX_SECOND));
            IsAnalyzing2.Select(x => x).Subscribe(async _ => await Run(PartyConst.PARTY_INDEX_THIRD));
            IsAnalyzing3.Select(x => x).Subscribe(async _ => await Run(PartyConst.PARTY_INDEX_FOUR));
            IsAnalyzing4.Select(x => x).Subscribe(async _ => await Run(PartyConst.PARTY_INDEX_FIFTH));
            IsAnalyzing5.Select(x => x).Subscribe(async _ => await Run(PartyConst.PARTY_INDEX_SIXTH));
        }

        private async Task Run(int partyIndex)
        {
            ReactiveProperty<bool> isAnalyzing = GetIsAnalyzingProperty(partyIndex);

            // 待機状態プロパティ情報取得
            var property = GetType().GetProperty(string.Format("WaitState{0}", partyIndex));

            while (isAnalyzing.Value)
            {
                property.SetValue(this, WaitStateConst.FIRST_STEP);
                await Task.Delay(400);
                property.SetValue(this, WaitStateConst.SECOND_STEP);
                await Task.Delay(400);
                property.SetValue(this, WaitStateConst.THIRD_STEP);
                await Task.Delay(400);
                property.SetValue(this, WaitStateConst.FOUR_STEP);
                await Task.Delay(400);
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

        public void End(int partyIndex)
        {
            // プロパティの値を取得
            ReactiveProperty<bool> isAnalyzing = GetIsAnalyzingProperty(partyIndex);

            isAnalyzing.Value = false;
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
    }
}
