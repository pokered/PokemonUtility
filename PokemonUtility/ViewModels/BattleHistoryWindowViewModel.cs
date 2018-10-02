using PokemonUtility.Models.BattleHistory;
using PokemonUtility.ViewModels.Abstract;

namespace PokemonUtility.ViewModels
{
    class BattleHistoryWindowViewModel : SubWindowViewModel
    {
        public BattleHistoryWindowViewModel() : base(BattleHistoryWindowModel.GetInstance())
        { }
    }
}
