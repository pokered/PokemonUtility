using System;
using PokemonUtility.Const;
using Prism.Mvvm;
using Reactive.Bindings;
using System.Threading.Tasks;
using System.Reflection;
using System.Reactive.Linq;

namespace PokemonUtility.Models.WaitState
{
    class PartyWaiStatetModel : BindableBase
    {
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
            IsAnalyzing1.Select(x => x).Subscribe(async _ => await Run(PartyConst.PARTY_INDEX1));
            IsAnalyzing2.Select(x => x).Subscribe(async _ => await Run(PartyConst.PARTY_INDEX2));
            IsAnalyzing3.Select(x => x).Subscribe(async _ => await Run(PartyConst.PARTY_INDEX3));
            IsAnalyzing4.Select(x => x).Subscribe(async _ => await Run(PartyConst.PARTY_INDEX4));
            IsAnalyzing5.Select(x => x).Subscribe(async _ => await Run(PartyConst.PARTY_INDEX5));
            IsAnalyzing6.Select(x => x).Subscribe(async _ => await Run(PartyConst.PARTY_INDEX6));
        }

        private async Task Run(int partyIndex)
        {
            while (IsAnalyzing2.Value)
            {
                await Task.Delay(400);
                WaitState2 = 0;
                await Task.Delay(400);
                WaitState2 = 1;
                await Task.Delay(400);
                WaitState2 = 2;
                await Task.Delay(400);
                WaitState2 = 3;
            }

            // 終了したら消す
            WaitState2 = -1;
        }

        public void Start(int partyIndex)
        {
            // プロパティ情報取得
            var property = GetType().GetProperty(string.Format("IsAnalyzing{0}", partyIndex));

            // プロパティが存在しなければ終了
            if (property == null) return;

            // プロパティの値を取得
            ReactiveProperty<bool> isAnalyzing = (ReactiveProperty<bool>)property.GetValue(this);

            isAnalyzing.Value = true;
        }

        public void End(int partyIndex)
        {
            // プロパティ情報取得
            var property = GetType().GetProperty(string.Format("IsAnalyzing{0}", partyIndex));

            // プロパティが存在しなければ終了
            if (property == null) return;

            // プロパティの値を取得
            ReactiveProperty<bool> isAnalyzing = (ReactiveProperty<bool>)property.GetValue(this);

            isAnalyzing.Value = false;
        }

        private PropertyInfo GetIsAnalyzingProperty(int partyIndex)
        {
            // プロパティ情報取得
            return GetType().GetProperty(string.Format("IsAnalyzing{0}", partyIndex));
        }

        private ReactiveProperty<bool> GetIsAnalyzingProperty1(int partyIndex)
        {
            // プロパティ情報取得
            var property = GetType().GetProperty(string.Format("IsAnalyzing{0}", partyIndex));

            // プロパティが存在しなければ終了
            if (property == null) return new ReactiveProperty<bool>(false);

            // プロパティの値を取得
            return (ReactiveProperty<bool>)property.GetValue(this);
        }


        //private async Task Run(int partyIndex)
        //{
        //    // プロパティ情報取得
        //    var property = GetType().GetProperty(string.Format("IsAnalyzing{0}", partyIndex));

        //    // プロパティが存在しなければ終了
        //    if (property == null) return;

        //    // プロパティの値を取得
        //    ReactiveProperty<bool> isAnalyzing = (ReactiveProperty<bool>)property.GetValue(this);


        //    // プロパティ情報取得
        //    var property2 = GetType().GetProperty(string.Format("WaitState{0}", partyIndex));

        //    // プロパティが存在しなければ終了
        //    if (property2 == null) return;

        //    while (isAnalyzing.Value)
        //    {
        //        property2.SetValue(this, 0);
        //        await Task.Delay(300);
        //        property2.SetValue(this, 1);
        //        await Task.Delay(300);
        //        property2.SetValue(this, 2);
        //        await Task.Delay(300);
        //        property2.SetValue(this, 3);
        //    }

        //    // 終了したら消す
        //    property2.SetValue(this, -1);
        //}
    }
}
