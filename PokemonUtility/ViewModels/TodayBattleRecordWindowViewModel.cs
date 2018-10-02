using PokemonUtility.Models.TodayBattleRecord;
using Prism.Mvvm;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace PokemonUtility.ViewModels
{
    public class TodayBattleRecordWindowViewModel : BindableBase
    {
        // ウィンドウ表示フラグ
        public ReactiveProperty<bool> IsShowWindow { get; private set; }

        // モデル
        private TodayBattleRecordWindowModel _todayBattleRecordWindowModel = TodayBattleRecordWindowModel.GetInstance();

        public TodayBattleRecordWindowViewModel()
        {
            IsShowWindow = _todayBattleRecordWindowModel.ObserveProperty(m => m.IsShowWindow).ToReactiveProperty();
        }
    }
}
