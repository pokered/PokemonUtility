using PokemonUtility.Models.TodayBattleRecord;
using PokemonUtility.ViewModels.Abstract;

namespace PokemonUtility.ViewModels
{
    class TodayBattleRecordWindowViewModel : SubWindowViewModel
    {
        public TodayBattleRecordWindowViewModel() : base(TodayBattleRecordWindowModel.GetInstance())
        { }
    }
}
